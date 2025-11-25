using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class AnimationLoader
{
    public static AnimationContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(AnimationConfig.ContentDirectory, "/", assetId, AnimationConfig.FileExtension);
        var reference = new ExternalReference<AnimationContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<AnimationContent, AnimationContent>(reference, AnimationConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Animation '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
