using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = TileMapConfig.ProcessorDisplayName)]
public sealed class TileMapProcessor : ContentProcessor<TileMapDto, TileMapContent>
{
    public override TileMapContent Process(TileMapDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new TileMapContent
        {
Id = input.Id,
Spritesheet = string.IsNullOrWhiteSpace(input.Spritesheet) ? null : SpriteSheetLoader.Load(input.Spritesheet, context),
BoundingBoxSheet = string.IsNullOrWhiteSpace(input.BoundingBoxSheet) ? null : BoundingBoxSheetLoader.Load(input.BoundingBoxSheet, context),
Tiles = input.Tiles,
};


return content;
    }

    private static void AssertValid(TileMapDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("TileMap payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("TileMap requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.Spritesheet))
        {
            throw new InvalidContentException("TileMap requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException("TileMap requires ''.");
        }

}
}
