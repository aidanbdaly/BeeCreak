using System;
using BeeCreak.Content.Pipeline.Extensions.SpriteSheet;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.EntityRecord;

[ContentProcessor(DisplayName = "Entity Record Processor")]
public sealed class EntityRecordProcessor : ContentProcessor<EntityRecordDto, EntityRecordContent>
{
    public override EntityRecordContent Process(EntityRecordDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity record payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity record requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.SpriteSheet))
        {
            throw new InvalidContentException($"Entity record '{input.Id}' requires a spritesheet id.");
        }

        var spriteSheet = BuildSpriteSheet(input.SpriteSheet, context, input.Id);

        var content = new EntityRecordContent
        {
            Id = input.Id,
            SpriteSheet = spriteSheet
        };

        if (input.Behaviours is not null)
        {
            foreach (var behaviour in input.Behaviours)
            {
                if (!string.IsNullOrWhiteSpace(behaviour))
                {
                    content.Behaviours.Add(behaviour);
                }
            }
        }

        return content;
    }

    private static SpriteSheetContent BuildSpriteSheet(string spriteSheetId, ContentProcessorContext context, string entityId)
    {
        var path = $"SpriteSheet/{spriteSheetId}.spritesheet";
        var reference = new ExternalReference<SpriteSheetContent>(path);

        try
        {
            return context.BuildAndLoadAsset<SpriteSheetContent, SpriteSheetContent>(reference, "SpriteSheetProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity record '{entityId}' failed to load sprite sheet '{spriteSheetId}': {ex.Message}", ex);
        }
    }
}
