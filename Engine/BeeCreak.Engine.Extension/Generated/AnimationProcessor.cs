using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "Animation Processor")]
public sealed class AnimationProcessor : ContentProcessor<AnimationDto, AnimationContent>
{
    public override AnimationContent Process(AnimationDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new AnimationContent
        {
Id = input.Id,
SpriteSheet = string.IsNullOrWhiteSpace(input.SpriteSheet) ? null : LoadAsset<SpriteSheetContent>(input.SpriteSheet, "SpriteSheet", "SpriteSheet", ".spritesheet", "SpriteSheetProcessor", context),
};


if (input.Data is not null)
        {
            foreach (var item in input.Data)
            {
content.Data.Add(item ?? string.Empty);
}
        }
return content;
    }

    private static void AssertValid(AnimationDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("Animation payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Animation requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.SpriteSheet))
        {
            throw new InvalidContentException("Animation requires ''.");
        }

        if (input.Data is null || input.Data.Count < 1)
        {
throw new InvalidContentException("Animation requires at least 1 '' entries.");
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
