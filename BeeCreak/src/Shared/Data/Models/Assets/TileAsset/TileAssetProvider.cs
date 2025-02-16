using BeeCreak.Shared.Data.Config;
using Microsoft.Extensions.Options;

namespace BeeCreak.Shared.Data.Models;

public class TileAssetProvider : GenericProvider<TileAsset, TileAssetDTO>, ITileAssetProvider
{
    public TileAssetProvider(GenericFactory<TileAsset, TileAssetDTO> factory, IOptions<AppSettings> options) : base(factory, options.Value.TileMetadataPath) { }

    public TileAsset GetTileAsset(string type, string variant = "Default")
    {
        return Get($"{type}_{variant}");
    }
}