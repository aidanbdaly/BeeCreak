namespace BeeCreak.Shared.Data.Models;

public static class TileAssetTypeExtensions
{
    public static bool HasVariants(this TileAssetType type)
    {
        return type switch
        {
            TileAssetType.GrassOnWater => true,
            _ => false,
        };
    }
}