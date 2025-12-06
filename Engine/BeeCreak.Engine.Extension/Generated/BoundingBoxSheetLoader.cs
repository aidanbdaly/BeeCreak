using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class BoundingBoxSheetLoader
{
    public static BoundingBoxSheetContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(BoundingBoxSheetConfig.ContentDirectory, "/", assetId, BoundingBoxSheetConfig.FileExtension);
        var reference = new ExternalReference<BoundingBoxSheetContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<BoundingBoxSheetContent, BoundingBoxSheetContent>(reference, BoundingBoxSheetConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"BoundingBoxSheet '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
