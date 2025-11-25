using NJsonSchema;

namespace BeeCreak.ExtensionGenerator
{
    public static class AssetModelBuilder
    {
        public static IReadOnlyList<AssetType> Build(JsonSchema schema)
        {
            var list = new List<AssetType>();

            foreach (var (name, defSchema) in schema.Definitions)
            {
                if (defSchema.Type != JsonObjectType.Object)
                    continue;

                var asset = new AssetType
                {
                    Name = name,
                    Properties = BuildProperties(defSchema)
                };

                list.Add(asset);
            }

            return list;
        }

        private static List<AssetProperty> BuildProperties(JsonSchema defSchema)
        {
            var props = new List<AssetProperty>();

            foreach (var (propName, propSchema) in defSchema.ActualProperties)
            {
                props.Add(new AssetProperty
                {
                    Name = ToPascalCase(propName),
                    CsType = ToCSharpType(propSchema),
                    IsRequired = defSchema.RequiredProperties.Contains(propName)
                });
            }

            return props;
        }

        private static string ToCSharpType(JsonSchema propertySchema)
        {
            var type = propertySchema.Type;

            bool nullable = propertySchema.IsNullable(SchemaType.JsonSchema);

            if (type.HasFlag(JsonObjectType.Array))
            {
                var itemType = propertySchema.Item != null ? ToCSharpType(propertySchema.Item) : "object";
                return $"List<{itemType}>";
            }

            return type switch
            {
                JsonObjectType.String => "string",
                JsonObjectType.Integer => nullable ? "int?" : "int",
                JsonObjectType.Number => nullable ? "double?" : "double",
                JsonObjectType.Boolean => nullable ? "bool?" : "bool",
                JsonObjectType.Object => propertySchema.Reference?.Title ?? "object",
                _ => "object"
            };
        }

        private static string ToPascalCase(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            if (name.Length == 1)
                return name.ToUpperInvariant();

            return char.ToUpperInvariant(name[0]) + name[1..];
        }
    }
}