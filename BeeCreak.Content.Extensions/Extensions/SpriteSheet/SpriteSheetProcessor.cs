using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.ContentPipeline;

[ContentProcessor(DisplayName = "Spritesheet Processor")]
internal sealed class SpritesheetProcessor : ContentProcessor<SpriteSheetDTO, SpriteSheetContent>
{
    public override SpriteSheetContent Process(SpriteSheetDTO input, ContentProcessorContext context)
    {
        var spriteSheet = new SpriteSheetContent
        {
            Image = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(new ExternalReference<Texture2DContent>(input.ImageName), "TextureProcessor"),
            Frames = input.Frames
        };

        return spriteSheet;
    }
}