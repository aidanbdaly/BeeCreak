using System;
using System.Collections.Generic;
using System.Linq;
using NJsonSchema;

namespace BeeCreak.ExtensionGenerator
{
    public static class AssetModelBuilder
    {
        private const string RuntimeNamespaceKey = "x-runtimeNamespace";
        private const string RuntimeAssemblyKey = "x-runtimeAssembly";
        private const string FileExtensionKey = "x-fileExtension";
        private const string ReferenceAssetKey = "x-referenceAsset";

        public static IReadOnlyList<AssetType> Build(
            JsonSchema schema,
            IReadOnlyDictionary<string, JsonSchema?>? referenceSchemas = null,
            IReadOnlyDictionary<string, AssetReferenceMetadata>? additionalMetadata = null)
        {
            var runtimeNamespace = GetExtensionDataString(schema.ExtensionData, RuntimeNamespaceKey) ?? "BeeCreak";
            var runtimeAssembly = GetExtensionDataString(schema.ExtensionData, RuntimeAssemblyKey) ?? "BeeCreak";
            var assetNames = new HashSet<string>(schema.Definitions.Keys, StringComparer.OrdinalIgnoreCase);
            var assetSchemas = new Dictionary<string, JsonSchema?>(StringComparer.OrdinalIgnoreCase);
            var assetMetadata = new Dictionary<string, AssetReferenceMetadata>(StringComparer.OrdinalIgnoreCase);
            foreach (var (name, defSchema) in schema.Definitions)
            {
                assetSchemas[name] = defSchema;
            }

            if (referenceSchemas is not null)
            {
                foreach (var (name, defSchema) in referenceSchemas)
                {
                    assetNames.Add(name);
                    assetSchemas[name] = defSchema;
                    if (defSchema is not null)
                    {
                        var ext = GetExtensionDataString(defSchema.ExtensionData, FileExtensionKey) ?? ".json";
                        assetMetadata[name] = new AssetReferenceMetadata(name, ext, $"{name}Processor", Array.Empty<string>());
                }
            }
            }

            if (additionalMetadata is not null)
            {
                foreach (var (name, metadata) in additionalMetadata)
                {
                    assetNames.Add(name);
                    assetMetadata[name] = metadata;
                }
            }

            var list = new List<AssetType>();

            foreach (var (name, defSchema) in schema.Definitions)
            {
                if (!IsObjectDefinition(defSchema))
                    continue;

                var fileExtension = GetExtensionDataString(defSchema.ExtensionData, FileExtensionKey) ?? ".json";
                assetMetadata[name] = new AssetReferenceMetadata(name, fileExtension, $"{name}Processor", Array.Empty<string>());

                var properties = BuildProperties(defSchema, assetNames, assetSchemas, assetMetadata);
                var dependencies = properties
                    .SelectMany(p => new[] { p.ReferenceAssetName, p.ElementReferenceAssetName })
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x!)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var asset = new AssetType
                {
                    Name = name,
                    FileExtension = fileExtension,
                    RuntimeNamespace = runtimeNamespace,
                    RuntimeAssembly = runtimeAssembly,
                    Properties = properties,
                    Dependencies = dependencies
                };

                list.Add(asset);
            }

            return list;
        }

        private static List<AssetProperty> BuildProperties(
            JsonSchema defSchema,
            ISet<string> assetNames,
            IReadOnlyDictionary<string, JsonSchema?> assetSchemas,
            IReadOnlyDictionary<string, AssetReferenceMetadata> assetMetadata)
        {
            var props = new List<AssetProperty>();

            foreach (var (propName, propSchema) in defSchema.ActualProperties)
            {
                var property = CreateProperty(
                    propName,
                    propSchema,
                    defSchema.RequiredProperties.Contains(propName),
                    assetNames,
                    assetSchemas,
                    assetMetadata);
                props.Add(property);
            }

            return props;
        }

        private static AssetProperty CreateProperty(
            string propName,
            JsonSchema propSchema,
            bool isRequired,
            ISet<string> assetNames,
            IReadOnlyDictionary<string, JsonSchema?> assetSchemas,
            IReadOnlyDictionary<string, AssetReferenceMetadata> assetMetadata)
        {
            var typeInfo = DescribeSchema(propSchema);
            var elementComplexProperties = BuildElementProperties(propSchema, assetNames, assetSchemas, assetMetadata);
            var property = new AssetProperty
            {
                Name = ToPascalCase(propName),
                JsonName = propName,
                CsType = typeInfo.TypeName,
                IsRequired = isRequired,
                IsArray = typeInfo.IsArray,
                IsDictionary = typeInfo.IsDictionary,
                ElementType = typeInfo.ElementTypeName,
                IsString = typeInfo.IsString,
                IsBoolean = typeInfo.IsBoolean,
                IsNumber = typeInfo.IsNumber,
                ElementIsString = typeInfo.ElementIsString,
                ElementIsBoolean = typeInfo.ElementIsBoolean,
                ElementIsNumber = typeInfo.ElementIsNumber,
                ElementIsCollection = typeInfo.ElementInfo is not null && (typeInfo.ElementInfo.IsArray || typeInfo.ElementInfo.IsDictionary),
                ElementCollectionIsArray = typeInfo.ElementInfo?.IsArray ?? false,
                ElementCollectionIsDictionary = typeInfo.ElementInfo?.IsDictionary ?? false,
                ElementCollectionElementType = typeInfo.ElementInfo?.ElementTypeName,
                ElementCollectionElementIsString = typeInfo.ElementInfo?.ElementIsString ?? false,
                ElementCollectionElementIsBoolean = typeInfo.ElementInfo?.ElementIsBoolean ?? false,
                ElementCollectionElementIsNumber = typeInfo.ElementInfo?.ElementIsNumber ?? false,
                MinItems = propSchema.MinItems,
                MinProperties = propSchema.MinProperties,
                ComplexProperties = BuildNestedProperties(propSchema, assetNames, assetSchemas, assetMetadata),
                ElementComplexProperties = elementComplexProperties,
                ElementIsComplex = elementComplexProperties is { Count: > 0 }
            };

            var referenceOverride = GetReferenceAssetHint(propSchema);
            var elementSchema = GetCollectionElementSchema(propSchema);
            var elementReferenceOverride = GetReferenceAssetHint(elementSchema);
            var referenceMatch = referenceOverride ?? FindAssetMatch(property.Name, assetNames);
            if (property.IsArray || property.IsDictionary)
            {
                property.ElementReferenceAssetName = elementReferenceOverride ?? referenceMatch;
            }
            else
            {
                property.ReferenceAssetName = referenceMatch;
            }

            if (!string.IsNullOrWhiteSpace(property.ReferenceAssetName))
            {
                var metadata = ResolveReferenceMetadata(property.ReferenceAssetName, assetMetadata, assetSchemas);
                property.ReferenceContentDirectory = metadata.ContentDirectory;
                property.ReferenceFileExtension = metadata.FileExtension;
                property.ReferenceProcessorName = metadata.ProcessorName;
                property.ReferenceNamespaces = metadata.Namespaces;
            }

            if (!string.IsNullOrWhiteSpace(property.ElementReferenceAssetName))
            {
                var metadata = ResolveReferenceMetadata(property.ElementReferenceAssetName, assetMetadata, assetSchemas);
                property.ElementReferenceContentDirectory = metadata.ContentDirectory;
                property.ElementReferenceFileExtension = metadata.FileExtension;
                property.ElementReferenceProcessorName = metadata.ProcessorName;
                property.ElementReferenceNamespaces = metadata.Namespaces;
            }

            if (property.ElementIsComplex)
            {
                var elementDtoTypeName = property.ElementComplexDtoTypeName;
                if (property.IsArray)
                {
                    property.CsType = $"List<{elementDtoTypeName}>";
                }
                else if (property.IsDictionary)
                {
                    property.CsType = $"Dictionary<string, {elementDtoTypeName}>";
                }
            }

            property.ContentCsType = BuildContentType(property);

            return property;
        }

        private static IReadOnlyList<AssetProperty>? BuildNestedProperties(
            JsonSchema schema,
            ISet<string> assetNames,
            IReadOnlyDictionary<string, JsonSchema?> assetSchemas,
            IReadOnlyDictionary<string, AssetReferenceMetadata> assetMetadata)
        {
            if (schema.ActualProperties.Count == 0)
            {
                return null;
            }

            var result = new List<AssetProperty>();
            foreach (var (name, nestedSchema) in schema.ActualProperties)
            {
                var nestedProperty = CreateProperty(
                    name,
                    nestedSchema,
                    schema.RequiredProperties.Contains(name),
                    assetNames,
                    assetSchemas,
                    assetMetadata);
                result.Add(nestedProperty);
            }

            return result;
        }

        private static IReadOnlyList<AssetProperty>? BuildElementProperties(
            JsonSchema schema,
            ISet<string> assetNames,
            IReadOnlyDictionary<string, JsonSchema?> assetSchemas,
            IReadOnlyDictionary<string, AssetReferenceMetadata> assetMetadata)
        {
            var elementSchema = GetCollectionElementSchema(schema);

            if (elementSchema is null)
            {
                return null;
            }

            return BuildNestedProperties(elementSchema, assetNames, assetSchemas, assetMetadata);
        }

        private static TypeInfo DescribeSchema(JsonSchema schema)
        {
            schema = schema.ActualSchema;

            if (schema.Type.HasFlag(JsonObjectType.Array))
            {
                var itemSchema = GetArrayItemSchema(schema);
                var elementInfo = itemSchema != null ? DescribeSchema(itemSchema) : TypeInfo.ForPrimitive("object");
                return TypeInfo.ForArray(elementInfo);
            }

            if (schema.AdditionalPropertiesSchema is { } additional)
            {
                var valueInfo = DescribeSchema(additional);
                return TypeInfo.ForDictionary(valueInfo);
            }

            if (schema.Reference is { } reference)
            {
                var title = string.IsNullOrWhiteSpace(reference.Title) ? "object" : reference.Title;
                return TypeInfo.ForClass(title);
            }

            var type = schema.Type;
            bool nullable = schema.IsNullable(SchemaType.JsonSchema);

            if (type.HasFlag(JsonObjectType.String))
            {
                return TypeInfo.ForPrimitive("string", isString: true);
            }

            if (type.HasFlag(JsonObjectType.Integer))
            {
                return TypeInfo.ForPrimitive(nullable ? "int?" : "int", isNumber: true);
            }

            if (type.HasFlag(JsonObjectType.Number))
            {
                return TypeInfo.ForPrimitive(nullable ? "double?" : "double", isNumber: true);
            }

            if (type.HasFlag(JsonObjectType.Boolean))
            {
                return TypeInfo.ForPrimitive(nullable ? "bool?" : "bool", isBoolean: true);
            }

            if (type.HasFlag(JsonObjectType.Object))
            {
                return TypeInfo.ForClass(schema.Title ?? "object");
            }

            return TypeInfo.ForClass("object");
        }

        private static bool IsObjectDefinition(JsonSchema schema)
        {
            return schema.Type.HasFlag(JsonObjectType.Object) || schema.ActualProperties.Count > 0;
        }

        private static string BuildContentType(AssetProperty property)
        {
            if (property.IsArray)
            {
                var elementType = property.IsElementReference
                    ? $"{property.ElementReferenceAssetName}Content"
                    : property.ElementIsComplex
                        ? property.ElementComplexContentTypeName
                    : property.ElementType ?? "object";

                return $"List<{elementType}>";
            }

            if (property.IsDictionary)
            {
                var valueType = property.IsElementReference
                    ? $"{property.ElementReferenceAssetName}Content"
                    : property.ElementIsComplex
                        ? property.ElementComplexContentTypeName
                    : property.ElementType ?? "object";

                return $"Dictionary<string, {valueType}>";
            }

            if (property.IsReference && property.ReferenceAssetName is { } reference)
            {
                return $"{reference}Content";
            }

            return property.CsType;
        }

        private static string? FindAssetMatch(string propertyName, ISet<string> assetNames)
        {
            foreach (var candidate in EnumerateNameCandidates(propertyName))
            {
                var match = assetNames.FirstOrDefault(asset =>
                    string.Equals(asset, candidate, StringComparison.OrdinalIgnoreCase));

                if (match is not null)
                {
                    return match;
                }
            }

            return null;
        }

        private static IEnumerable<string> EnumerateNameCandidates(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                yield break;
            }

            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var baseName in EnumerateBaseNameCandidates(name))
            {
                foreach (var candidate in EnumeratePluralCandidates(baseName))
                {
                    if (seen.Add(candidate))
                    {
                        yield return candidate;
                    }
                }
            }
        }

        private static IEnumerable<string> EnumerateBaseNameCandidates(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                yield return name;
            }

            if (name.EndsWith("Array", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = name[..^"Array".Length];
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    yield return trimmed;
                }
            }

            if (name.EndsWith("Dictionary", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = name[..^"Dictionary".Length];
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    yield return trimmed;
                }
            }
        }

        private static IEnumerable<string> EnumeratePluralCandidates(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                yield break;
            }

            yield return name;

            if (name.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
            {
                yield return name[..^3] + "y";
            }

            if (name.EndsWith("es", StringComparison.OrdinalIgnoreCase))
            {
                yield return name[..^2];
            }

            if (name.EndsWith("s", StringComparison.OrdinalIgnoreCase))
            {
                yield return name[..^1];
            }
        }

        private static string ToPascalCase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            if (name.Length == 1)
                return name.ToUpperInvariant();

            return char.ToUpperInvariant(name[0]) + name[1..];
        }

        private static AssetReferenceMetadata ResolveReferenceMetadata(
            string? assetName,
            IReadOnlyDictionary<string, AssetReferenceMetadata> assetMetadata,
            IReadOnlyDictionary<string, JsonSchema?> assetSchemas)
        {
            if (!string.IsNullOrWhiteSpace(assetName) &&
                assetMetadata.TryGetValue(assetName, out var metadata))
            {
                return metadata;
            }

            if (!string.IsNullOrWhiteSpace(assetName) &&
                assetSchemas.TryGetValue(assetName, out var schema) &&
                schema is not null)
            {
                var fileExtension = GetExtensionDataString(schema.ExtensionData, FileExtensionKey) ?? ".json";
                return new AssetReferenceMetadata(assetName!, fileExtension, $"{assetName}Processor", Array.Empty<string>());
            }

            var fallbackName = assetName ?? "Asset";
            return new AssetReferenceMetadata(fallbackName, ".json", $"{fallbackName}Processor", Array.Empty<string>());
        }

        private static string? GetExtensionDataString(
            IDictionary<string, object>? extensionData,
            string key)
        {
            if (extensionData is null)
            {
                return null;
            }

            if (extensionData.TryGetValue(key, out var value) && value is not null)
            {
                return value.ToString();
            }

            return null;
        }

        private static JsonSchema? GetArrayItemSchema(JsonSchema schema)
        {
            if (schema.Item is { } item)
            {
                return item;
            }

            if (schema.Items is { Count: > 0 })
            {
                return schema.Items.FirstOrDefault();
            }

            return schema.ActualProperties.Values.FirstOrDefault();
        }

        private static JsonSchema? GetCollectionElementSchema(JsonSchema schema)
        {
            if (schema.Type.HasFlag(JsonObjectType.Array))
            {
                return GetArrayItemSchema(schema);
            }

            if (schema.AdditionalPropertiesSchema is { } additional)
            {
                return additional;
            }

            return null;
        }

        private static string? GetReferenceAssetHint(JsonSchema? schema)
        {
            if (schema is null)
            {
                return null;
            }

            return GetExtensionDataString(schema.ExtensionData, ReferenceAssetKey);
        }

        private sealed class TypeInfo
        {
            public string TypeName { get; init; } = "object";

            public bool IsString { get; init; }

            public bool IsBoolean { get; init; }

            public bool IsNumber { get; init; }

            public bool IsArray { get; init; }

            public bool IsDictionary { get; init; }

            public string? ElementTypeName { get; init; }

            public bool ElementIsString { get; init; }

            public bool ElementIsBoolean { get; init; }

            public bool ElementIsNumber { get; init; }

            public TypeInfo? ElementInfo { get; init; }

            public static TypeInfo ForPrimitive(string typeName, bool isString = false, bool isBoolean = false, bool isNumber = false)
            {
                return new TypeInfo
                {
                    TypeName = typeName,
                    IsString = isString,
                    IsBoolean = isBoolean,
                    IsNumber = isNumber
                };
            }

            public static TypeInfo ForArray(TypeInfo element)
            {
                return new TypeInfo
                {
                    TypeName = $"List<{element.TypeName}>",
                    IsArray = true,
                    ElementTypeName = element.TypeName,
                    ElementIsString = element.IsString,
                    ElementIsBoolean = element.IsBoolean,
                    ElementIsNumber = element.IsNumber,
                    ElementInfo = element
                };
            }

            public static TypeInfo ForDictionary(TypeInfo element)
            {
                return new TypeInfo
                {
                    TypeName = $"Dictionary<string, {element.TypeName}>",
                    IsDictionary = true,
                    ElementTypeName = element.TypeName,
                    ElementIsString = element.IsString,
                    ElementIsBoolean = element.IsBoolean,
                    ElementIsNumber = element.IsNumber,
                    ElementInfo = element
                };
            }

            public static TypeInfo ForClass(string typeName)
            {
                return new TypeInfo
                {
                    TypeName = typeName
                };
            }
        }
    }
}
