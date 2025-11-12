using System;
using BeeCreak.Extensions.AnimationSheet;
using BeeCreak.Extensions.BoundingBoxSheet;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.EntityRecord;

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

        if (string.IsNullOrWhiteSpace(input.AnimationSheet))
        {
            throw new InvalidContentException($"Entity record '{input.Id}' requires an animationSheet id.");
        }

        if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException($"Entity record '{input.Id}' requires a boundingBoxSheet id.");
        }

        var animationSheet = BuildAnimationSheet(input.AnimationSheet, context, input.Id);
        var boundingBoxSheet = BuildBoundingBoxSheet(input.BoundingBoxSheet, context, input.Id);

        var content = new EntityRecordContent
        {
            Id = input.Id,
            AnimationSheet = animationSheet,
            BoundingBoxSheet = boundingBoxSheet
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

    private static AnimationSheetContent BuildAnimationSheet(string animationSheetId, ContentProcessorContext context, string entityId)
    {
        var path = $"AnimationSheet/{animationSheetId}.as";
        var reference = new ExternalReference<AnimationSheetContent>(path);

        try
        {
            return context.BuildAndLoadAsset<AnimationSheetContent, AnimationSheetContent>(reference, "AnimationSheetProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity record '{entityId}' failed to load animation sheet '{animationSheetId}': {ex.Message}", ex);
        }
    }

    private static BoundingBoxSheetContent BuildBoundingBoxSheet(string sheetId, ContentProcessorContext context, string entityId)
    {
        var path = $"BoundingBoxSheet/{sheetId}.bbs";
        var reference = new ExternalReference<BoundingBoxSheetContent>(path);

        try
        {
            return context.BuildAndLoadAsset<BoundingBoxSheetContent, BoundingBoxSheetContent>(reference, "BoundingBoxSheetProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Entity record '{entityId}' failed to load bounding box sheet '{sheetId}': {ex.Message}", ex);
        }
    }
}
