using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = AnimationConfig.ProcessorDisplayName)]
public sealed class AnimationProcessor : ContentProcessor<AnimationDto, AnimationContent>
{
    public override AnimationContent Process(AnimationDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new AnimationContent
        {
Id = input.Id,
SpriteSheet = string.IsNullOrWhiteSpace(input.SpriteSheet) ? null : SpriteSheetLoader.Load(input.SpriteSheet, context),
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
}
