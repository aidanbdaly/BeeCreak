using System.Linq;
using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.ContentPipeline;

[ContentProcessor(DisplayName = "Animation Processor")]
internal sealed class AnimationProcessor : ContentProcessor<AnimationDTO, AnimationContent>
{
    public override AnimationContent Process(AnimationDTO input, ContentProcessorContext context)
    {
        var animation = new AnimationContent
        {
            Image = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(new ExternalReference<Texture2DContent>(input.ImageName), "TextureProcessor"),
            TimePerFrame = input.TimePerFrame,
            Loop = input.Loop,
            Frames = input.Frames  
        };

        return animation;
    }
}