using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

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
Texture = string.IsNullOrWhiteSpace(input.Texture) ? null : LoadAsset<TextureContent>(input.Texture, "Texture", "Image", ".png", "TextureProcessor", context),
};


if (input.Data is not null)
        {
            foreach (var item in input.Data)
            {
content.Data.Add(MapDataEntry(item, context));
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

        if (input.Data is null || input.Data.Count < 1)
        {
throw new InvalidContentException("Animation requires at least 1 '' entries.");
}

}

private static AnimationContent.DataEntryContent MapDataEntry(AnimationDto.DataEntryDto? input, ContentProcessorContext context)
    {
        if (input is null)
        {
            return new AnimationContent.DataEntryContent();
        }

        var content = new AnimationContent.DataEntryContent();
content.X = input.X;
content.Y = input.Y;
content.W = input.W;
content.H = input.H;
return content;
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
