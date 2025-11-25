using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class TextSetLoader
{
    public static TextSetContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(TextSetConfig.ContentDirectory, "/", assetId, TextSetConfig.FileExtension);
        var reference = new ExternalReference<TextSetContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<TextSetContent, TextSetContent>(reference, TextSetConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"TextSet '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
