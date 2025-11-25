using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Model;

public class EntityModelLoader
{
    public static EntityModelContent Load(string entityModelId, ContentProcessorContext context)
    {
        var reference = new ExternalReference<EntityModelContent>($"{EntityModelConfig.ContentDirectory}/{entityModelId}{EntityModelConfig.FileExtension}");

        try
        {
            return context.BuildAndLoadAsset<EntityModelContent, EntityModelContent>(reference, EntityModelConfig.ProcessorDisplayName);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity reference failed to load base entity '{entityModelId}': {ex.Message}", ex);
        }
    }
}