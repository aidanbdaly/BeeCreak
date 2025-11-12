using System;
using BeeCreak.Extensions.EntityRecord;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.EntityReference;

[ContentProcessor(DisplayName = "Entity Reference Processor")]
public sealed class EntityReferenceProcessor : ContentProcessor<EntityReferenceDto, EntityReferenceContent>
{
    public override EntityReferenceContent Process(EntityReferenceDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity reference payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity reference requires an id.");
        }

        if (input.Position is null)
        {
            throw new InvalidContentException($"Entity reference '{input.Id}' requires a position.");
        }

        var baseEntity = LoadEntityRecord(input.Base, context, input.Id);

        var content = new EntityReferenceContent
        {
            Id = input.Id,
            BaseEntity = baseEntity,
            Variant = string.IsNullOrWhiteSpace(input.Variant) ? "default" : input.Variant,
            Position = new Vector2(input.Position.X, input.Position.Y)
        };

        return content;
    }

    private static EntityRecordContent LoadEntityRecord(string entityId, ContentProcessorContext context, string referenceId)
    {
        if (string.IsNullOrWhiteSpace(entityId))
        {
            throw new InvalidContentException($"Entity reference '{referenceId}' requires a base entity id.");
        }

        var reference = new ExternalReference<EntityRecordContent>($"EntityRecord/{entityId}.erec");
        try
        {
            return context.BuildAndLoadAsset<EntityRecordContent, EntityRecordContent>(reference, "EntityRecordProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity reference '{referenceId}' failed to load base entity '{entityId}': {ex.Message}", ex);
        }
    }
}
