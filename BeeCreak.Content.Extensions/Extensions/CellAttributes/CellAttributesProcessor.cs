

using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Cell Attributes Processor")]

public sealed class CellAttributesProcessor : ContentProcessor<CellAttributesDTO, CellAttributesContent>
{
    public override CellAttributesContent Process(CellAttributesDTO input, ContentProcessorContext context)
    {
        var tileMapContent = new TileMapContent
        {
            Width = input.TileMap.Width,
            Height = input.TileMap.Height,
            Tiles = input.TileMap.Tiles
        };

        var cellAttributesContent = new CellAttributesContent
        {
            Tint = input.Tint,
            LengthOfDay = input.LengthOfDay,
            LengthOfNight = input.LengthOfNight,
            TileMap = tileMapContent
        };

        return cellAttributesContent;
    }
}