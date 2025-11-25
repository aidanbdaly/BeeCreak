using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class SpriteSheetLoader
{
    public static SpriteSheetContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(SpriteSheetConfig.ContentDirectory, "/", assetId, SpriteSheetConfig.FileExtension);
        var reference = new ExternalReference<SpriteSheetContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<SpriteSheetContent, SpriteSheetContent>(reference, SpriteSheetConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"SpriteSheet '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
