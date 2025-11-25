using BeeCreak.Extensions.SpriteSheet;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Animation;

[ContentProcessor(DisplayName = "Animation Processor")]
public sealed class AnimationProcessor : ContentProcessor<AnimationDto, AnimationContent>
{
    public override AnimationContent Process(AnimationDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Animation payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Animation requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.SpriteSheet))
        {
            throw new InvalidContentException($"Animation '{input.Id}' requires a spritesheet.");
        }

        if (input.Data is null || input.Data.Count == 0)
        {
            throw new InvalidContentException($"Animation '{input.Id}' requires at least one spritename");
        }

        var spriteSheet = context.BuildAndLoadAsset<SpriteSheetContent, SpriteSheetContent>(new ExternalReference<SpriteSheetContent>(input.SpriteSheet), "SpriteSheetProcessor");

        var content = new AnimationContent
        {
            Id = input.Id,
            SpriteSheet = spriteSheet
        };

        foreach (var spriteName in input.Data)
        {
            if (string.IsNullOrWhiteSpace(spriteName))
            {
                throw new InvalidContentException($"Animation '{input.Id}' contains an empty spriteName");
            }

            content.Data.Add(spriteName);
        }

        return content;
    }
}
