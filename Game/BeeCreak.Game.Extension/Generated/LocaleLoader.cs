using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class LocaleLoader
{
    public static LocaleContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(LocaleConfig.ContentDirectory, "/", assetId, LocaleConfig.FileExtension);
        var reference = new ExternalReference<LocaleContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<LocaleContent, LocaleContent>(reference, LocaleConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Locale '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
