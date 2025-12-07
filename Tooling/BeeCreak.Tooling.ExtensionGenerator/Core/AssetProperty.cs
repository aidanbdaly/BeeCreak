using System;
using System.Collections.Generic;

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

        public bool ElementIsCollection { get; init; }

        public bool ElementCollectionIsArray { get; init; }

        public bool ElementCollectionIsDictionary { get; init; }

        public string? ElementCollectionElementType { get; init; }

        public bool ElementCollectionElementIsString { get; init; }

        public bool ElementCollectionElementIsBoolean { get; init; }

        public bool ElementCollectionElementIsNumber { get; init; }

        public bool ElementIsPrimitive => ElementIsString || ElementIsBoolean || ElementIsNumber;

        public bool ElementCollectionElementIsPrimitive =>
            ElementCollectionElementIsString || ElementCollectionElementIsBoolean || ElementCollectionElementIsNumber;

        public string? ReferenceAssetName { get; set; }

        public string? ElementReferenceAssetName { get; set; }

        public bool IsReference => !string.IsNullOrWhiteSpace(ReferenceAssetName);

        public bool IsElementReference => !string.IsNullOrWhiteSpace(ElementReferenceAssetName);
        public string ReferenceContentDirectory { get; set; } = "";

        public string ReferenceFileExtension { get; set; } = ".json";

        public string ReferenceProcessorName { get; set; } = "";

        public IReadOnlyCollection<string> ReferenceNamespaces { get; set; } = Array.Empty<string>();

        public string ElementReferenceContentDirectory { get; set; } = "";

        public string ElementReferenceFileExtension { get; set; } = ".json";

        public string ElementReferenceProcessorName { get; set; } = "";

        public IReadOnlyCollection<string> ElementReferenceNamespaces { get; set; } = Array.Empty<string>();

        public string ContentCsType { get; set; } = "";

        public int? MinItems { get; init; }

        public int? MinProperties { get; init; }

        public IReadOnlyList<AssetProperty>? ComplexProperties { get; set; }

        public bool IsComplex => ComplexProperties is not null && ComplexProperties.Count > 0;
    }
}
