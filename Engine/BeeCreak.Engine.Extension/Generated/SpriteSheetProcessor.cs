using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = SpriteSheetConfig.ProcessorDisplayName)]
public sealed class SpriteSheetProcessor : ContentProcessor<SpriteSheetDto, SpriteSheetContent>
{
    public override SpriteSheetContent Process(SpriteSheetDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new SpriteSheetContent
        {
Id = input.Id,
Image = input.Image,
};


if (input.Sprites is not null)
        {
            foreach (var entry in input.Sprites)
            {
content.Sprites[entry.Key] = entry.Value;
}
        }
return content;
    }

    private static void AssertValid(SpriteSheetDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("SpriteSheet payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("SpriteSheet requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.Image))
        {
            throw new InvalidContentException("SpriteSheet requires ''.");
        }

        if (input.Sprites is null || input.Sprites.Count < 1)
        {
throw new InvalidContentException("SpriteSheet requires at least 1 '' entries.");
}
}
}
