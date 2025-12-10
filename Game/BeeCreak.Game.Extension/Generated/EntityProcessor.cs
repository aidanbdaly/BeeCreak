using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "Entity Processor")]
public sealed class EntityProcessor : ContentProcessor<EntityDto, EntityContent>
{
    public override EntityContent Process(EntityDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new EntityContent
        {
Id = input.Id,
AnimationCollection = string.IsNullOrWhiteSpace(input.AnimationCollection) ? null : LoadAsset<AnimationCollectionContent>(input.AnimationCollection, "AnimationCollection", "AnimationCollection", ".asc", "AnimationCollectionProcessor", context),
BoundingBoxSheet = string.IsNullOrWhiteSpace(input.BoundingBoxSheet) ? null : LoadAsset<BoundingBoxSheetContent>(input.BoundingBoxSheet, "BoundingBoxSheet", "BoundingBoxSheet", ".bbs", "BoundingBoxSheetProcessor", context),
};


if (input.Behaviours is not null)
        {
            foreach (var item in input.Behaviours)
            {
content.Behaviours.Add(item ?? string.Empty);
}
        }
return content;
    }

    private static void AssertValid(EntityDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException("Entity requires ''.");
        }

        if (input.Behaviours is null)
        {
throw new InvalidContentException("Entity requires ''.");
}

}

private static TContent LoadAsset<TContent>(
        string assetId,
        string assetName,
        string directory,
        string extension,
        string processor,
        ContentProcessorContext context)
    {
        if (string.IsNullOrWhiteSpace(assetId))
        {
            throw new InvalidContentException($"{assetName} reference is empty.");
        }

        var assetPath = string.Concat(directory, "/", assetId, extension);
        var reference = new ExternalReference<TContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<TContent, TContent>(reference, processor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"{assetName} '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
