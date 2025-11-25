using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class EntityReferenceLoader
{
    public static EntityReferenceContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(EntityReferenceConfig.ContentDirectory, "/", assetId, EntityReferenceConfig.FileExtension);
        var reference = new ExternalReference<EntityReferenceContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<EntityReferenceContent, EntityReferenceContent>(reference, EntityReferenceConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"EntityReference '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
