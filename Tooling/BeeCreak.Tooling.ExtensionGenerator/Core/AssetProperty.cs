namespace BeeCreak.ExtensionGenerator
{
    public class AssetProperty
    {
        public string Name { get; init; } = "";

        public string JsonName { get; init; } = "";

        public string CsType { get; init; } = "";

        public bool IsRequired { get; init; }

        public bool IsArray { get; init; }

        public bool IsDictionary { get; init; }

        public string? ElementType { get; init; }

        public bool IsString { get; init; }

        public bool IsBoolean { get; init; }

        public bool IsNumber { get; init; }

        public bool IsPrimitive => IsString || IsBoolean || IsNumber;

        public bool ElementIsString { get; init; }

        public bool ElementIsBoolean { get; init; }

        public bool ElementIsNumber { get; init; }

        public bool IsCollection => IsArray || IsDictionary;

        public bool ElementIsPrimitive => ElementIsString || ElementIsBoolean || ElementIsNumber;

        public string? ReferenceAssetName { get; set; }

        public string? ElementReferenceAssetName { get; set; }

        public bool IsReference => !string.IsNullOrWhiteSpace(ReferenceAssetName);

        public bool IsElementReference => !string.IsNullOrWhiteSpace(ElementReferenceAssetName);

        public string ContentCsType { get; set; } = "";

        public int? MinItems { get; init; }

        public int? MinProperties { get; init; }

        public bool IsComplex => !IsPrimitive && !IsCollection;
    }
}
