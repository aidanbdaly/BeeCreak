using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.AnimationSheet;

[ContentProcessor(DisplayName = "Animation Sheet Processor")]
public sealed class AnimationSheetProcessor : ContentProcessor<AnimationSheetDto, AnimationSheetContent>
{
    public override AnimationSheetContent Process(AnimationSheetDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Animation sheet payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Animation sheet requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.Image))
        {
            throw new InvalidContentException($"Animation sheet '{input.Id}' requires an image.");
        }

        if (input.Animations is null || input.Animations.Count == 0)
        {
            throw new InvalidContentException($"Animation sheet '{input.Id}' requires at least one animation.");
        }

        var content = new AnimationSheetContent
        {
            Id = input.Id,
            Image = input.Image
        };

        foreach (var (animationId, frames) in input.Animations)
        {
            if (string.IsNullOrWhiteSpace(animationId))
            {
                throw new InvalidContentException($"Animation sheet '{input.Id}' contains an animation with an empty key.");
            }

            if (frames is null || frames.Count == 0)
            {
                throw new InvalidContentException($"Animation '{animationId}' in sheet '{input.Id}' requires at least one frame.");
            }

            var rectangles = new List<Rectangle>(frames.Count);
            foreach (var frame in frames)
            {
                rectangles.Add(new Rectangle(frame.X, frame.Y, frame.W, frame.H));
            }

            content.Animations[animationId] = rectangles;
        }

        return content;
    }
}
