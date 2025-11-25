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

        public static IReadOnlyList<AssetType> Build(JsonSchema schema)
        {
            var runtimeNamespace = GetExtensionDataString(schema.ExtensionData, RuntimeNamespaceKey) ?? "BeeCreak.Game";
            var runtimeAssembly = GetExtensionDataString(schema.ExtensionData, RuntimeAssemblyKey) ?? "BeeCreak";
            var assetNames = new HashSet<string>(schema.Definitions.Keys, StringComparer.OrdinalIgnoreCase);

            var list = new List<AssetType>();

            foreach (var (name, defSchema) in schema.Definitions)
            {
                if (!IsObjectDefinition(defSchema))
                    continue;

                var properties = BuildProperties(defSchema, assetNames);
                var dependencies = properties
                    .SelectMany(p => new[] { p.ReferenceAssetName, p.ElementReferenceAssetName })
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x!)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var asset = new AssetType
                {
                    Name = name,
                    FileExtension = GetExtensionDataString(defSchema.ExtensionData, FileExtensionKey) ?? ".json",
                    RuntimeNamespace = runtimeNamespace,
                    RuntimeAssembly = runtimeAssembly,
                    Properties = properties,
                    Dependencies = dependencies
                };

                list.Add(asset);
            }

            return list;
        }

        private static List<AssetProperty> BuildProperties(JsonSchema defSchema, ISet<string> assetNames)
        {
            var props = new List<AssetProperty>();

            foreach (var (propName, propSchema) in defSchema.ActualProperties)
            {
                var property = CreateProperty(propName, propSchema, defSchema.RequiredProperties.Contains(propName), assetNames);
                props.Add(property);
            }

            return props;
        }

        private static AssetProperty CreateProperty(
            string propName,
            JsonSchema propSchema,
            bool isRequired,
            ISet<string> assetNames)
        {
            var typeInfo = DescribeSchema(propSchema);
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
                MinItems = propSchema.MinItems,
                MinProperties = propSchema.MinProperties
            };

            var referenceMatch = FindAssetMatch(property.Name, assetNames);
            if (property.IsArray || property.IsDictionary)
            {
                property.ElementReferenceAssetName = referenceMatch;
            }
            else
            {
                property.ReferenceAssetName = referenceMatch;
            }

            property.ContentCsType = BuildContentType(property);

            return property;
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
                    : property.ElementType ?? "object";

                return $"List<{elementType}>";
            }

            if (property.IsDictionary)
            {
                var valueType = property.IsElementReference
                    ? $"{property.ElementReferenceAssetName}Content"
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
                    ElementIsNumber = element.IsNumber
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
                    ElementIsNumber = element.IsNumber
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
