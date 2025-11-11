using BeeCreak.Content.Pipeline.Extensions.CellReference;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.Game;

[ContentProcessor(DisplayName = "Game Record Processor")]
public sealed class GameRecordProcessor : ContentProcessor<GameRecordDto, GameRecordContent>
{
    public override GameRecordContent Process(GameRecordDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Game record payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.ActiveCell))
        {
            throw new InvalidContentException("Game record requires an activeCell id.");
        }

        var cellReference = context.BuildAndLoadAsset<CellReferenceContent, CellReferenceContent>(
            new ExternalReference<CellReferenceContent>($"CellReference/{input.ActiveCell}.cref"),
            "CellReferenceProcessor");

        return new GameRecordContent
        {
            ActiveCell = cellReference
        };
    }
}
