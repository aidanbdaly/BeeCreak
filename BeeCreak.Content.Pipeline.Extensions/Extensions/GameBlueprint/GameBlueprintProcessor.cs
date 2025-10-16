using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Game Blueprint Processor")]
public sealed class GameBlueprintProcessor : ContentProcessor<GameBlueprintDTO, GameBlueprintContent>
{
    public override GameBlueprintContent Process(GameBlueprintDTO input, ContentProcessorContext context)
    {
        var gameBlueprintContent = new GameBlueprintContent
        {
            ActiveCellId = input.ActiveCellId,
            Cells = input.Cells
        };

        return gameBlueprintContent;
    }
}
