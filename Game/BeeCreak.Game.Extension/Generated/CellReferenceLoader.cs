using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class CellReferenceLoader
{
    public static CellReferenceContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(CellReferenceConfig.ContentDirectory, "/", assetId, CellReferenceConfig.FileExtension);
        var reference = new ExternalReference<CellReferenceContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<CellReferenceContent, CellReferenceContent>(reference, CellReferenceConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"CellReference '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
