using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class TileMapLoader
{
    public static TileMapContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(TileMapConfig.ContentDirectory, "/", assetId, TileMapConfig.FileExtension);
        var reference = new ExternalReference<TileMapContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<TileMapContent, TileMapContent>(reference, TileMapConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"TileMap '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
