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

        if (string.IsNullOrWhiteSpace(input.ActiveCellId))
        {
            throw new InvalidContentException("Game record requires an ActiveCellId.");
        }

        return new GameRecordContent
        {
            ActiveCellId = input.ActiveCellId
        };
    }
}
