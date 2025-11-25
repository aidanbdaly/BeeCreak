using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class EntityModelLoader
{
    public static EntityModelContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(EntityModelConfig.ContentDirectory, "/", assetId, EntityModelConfig.FileExtension);
        var reference = new ExternalReference<EntityModelContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<EntityModelContent, EntityModelContent>(reference, EntityModelConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"EntityModel '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
