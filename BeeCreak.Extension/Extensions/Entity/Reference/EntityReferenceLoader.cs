using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Reference;

public class EntityReferenceLoader
{
    public static EntityReferenceContent Load(string entityModelId, ContentProcessorContext context)
    {
        var reference = new ExternalReference<EntityReferenceContent>($"{EntityReferenceConfig.ContentDirectory}/{entityModelId}{EntityReferenceConfig.FileExtension}");

        try
        {
            return context.BuildAndLoadAsset<EntityReferenceContent, EntityReferenceContent>(reference, EntityReferenceConfig.ProcessorDisplayName);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity reference failed to load base entity '{entityModelId}': {ex.Message}", ex);
        }
    }
}