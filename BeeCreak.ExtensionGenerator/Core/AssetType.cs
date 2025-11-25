using System.Collections.Generic;

namespace BeeCreak.ExtensionGenerator
{
    public class AssetType
    {
        public string Name { get; init; } = "";

        public string FileExtension { get; init; } = "";

        public string RuntimeNamespace { get; init; } = "";

        public string RuntimeAssembly { get; init; } = "";

        public IReadOnlyList<AssetProperty> Properties { get; init; } = [];

        public IReadOnlyCollection<string> Dependencies { get; init; } = [];
    }
}
