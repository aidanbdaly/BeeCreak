using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Animation Processor")]
public sealed class AnimationProcessor : ContentProcessor<AnimationDTO, AnimationContent>
{
    public override AnimationContent Process(AnimationDTO input, ContentProcessorContext context)
    {
        var animation = new AnimationContent
        {
            Image = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(new ExternalReference<Texture2DContent>(input.ImageName), "TextureProcessor"),
            TimePerFrame = input.TimePerFrame,
            Frames = input.Frames
        };

        return animation;
    }
}