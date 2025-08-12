using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Cell Blueprint Processor")]
public sealed class CellBlueprintProcessor : ContentProcessor<CellBlueprintDTO, CellBlueprintContent>
{
    public override CellBlueprintContent Process(CellBlueprintDTO input, ContentProcessorContext context)
    {
        var tileMapContent = new BlueprintTileMapContent
        {
            Width = input.TileMap.Width,
            Height = input.TileMap.Height,
            Data = input.TileMap.Data
        };

        var cellBlueprintContent = new CellBlueprintContent
        {
            TileMap = tileMapContent,
            Entities = input.Entities
        };

        return cellBlueprintContent;
    }
}
