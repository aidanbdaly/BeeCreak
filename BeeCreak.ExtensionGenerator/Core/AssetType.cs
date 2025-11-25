namespace BeeCreak.ExtensionGenerator
{
    public class AssetType
    {
        public string Name { get; init; } = "";
        
        public IReadOnlyList<AssetProperty> Properties { get; init; } = [];
    }
}
