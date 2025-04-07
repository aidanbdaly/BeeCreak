using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.ContentPipeline;

[ContentProcessor(DisplayName = "Spritesheet Processor")]
internal sealed class SpritesheetProcessor : ContentProcessor<SpriteSheetDTO, SpriteSheet>
{
    public override SpriteSheet Process(SpriteSheetDTO input, ContentProcessorContext context)
    {
        var spriteSheet = new SpriteSheet
        {
            Image = context.BuildAndLoadAsset<Texture2D, Texture2D>(input.SpriteSheetName, "TextureProcessor"),
            Frames = input.Frames
        };

        return spriteSheet;
    }
}