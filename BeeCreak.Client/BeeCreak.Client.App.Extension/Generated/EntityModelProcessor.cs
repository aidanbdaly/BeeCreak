using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "EntityModel Processor")]
public sealed class EntityModelProcessor : ContentProcessor<EntityModelDto, EntityModelContent>
{
    public override EntityModelContent Process(EntityModelDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new EntityModelContent
        {
Id = input.Id,
BoundingBoxSheet = string.IsNullOrWhiteSpace(input.BoundingBoxSheet) ? null : LoadAsset<BoundingBoxSheetContent>(input.BoundingBoxSheet, "BoundingBoxSheet", "BoundingBoxSheet", ".bbs", "BoundingBoxSheetProcessor", context),
};


if (input.Animations is not null)
        {
            foreach (var item in input.Animations)
            {
if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
content.Animations.Add(LoadAsset<AnimationContent>(item, "Animation", "Animation", ".as", "AnimationProcessor", context));
}
        }
if (input.Behaviours is not null)
        {
            foreach (var item in input.Behaviours)
            {
content.Behaviours.Add(item ?? string.Empty);
}
        }
return content;
    }

    private static void AssertValid(EntityModelDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("EntityModel payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("EntityModel requires ''.");
        }

        if (input.Animations is null || input.Animations.Count < 1)
        {
throw new InvalidContentException("EntityModel requires at least 1 '' entries.");
}

if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException("EntityModel requires ''.");
        }

        if (input.Behaviours is null)
        {
throw new InvalidContentException("EntityModel requires ''.");
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
