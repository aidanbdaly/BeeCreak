using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "SpriteSheet Processor")]
public sealed class SpriteSheetProcessor : ContentProcessor<SpriteSheetDTO, SpriteSheetContent>
{
    public override SpriteSheetContent Process(SpriteSheetDTO input, ContentProcessorContext context)
    {
        var spriteSheet = new SpriteSheetContent
        {
            Image = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(new ExternalReference<Texture2DContent>(input.ImageName), "TextureProcessor"),
            Resolution = input.Resolution,
            Frames = input.Frames
        };

        return spriteSheet;
    }
}