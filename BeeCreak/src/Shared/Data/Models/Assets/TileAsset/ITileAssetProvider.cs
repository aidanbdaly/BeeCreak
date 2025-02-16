namespace BeeCreak.Shared.Data.Models;

public interface ITileAssetProvider
{   
    TileAsset GetTileAsset(string tileType, string tileVariant);
}