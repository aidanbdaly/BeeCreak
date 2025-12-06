using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = GameRecordConfig.ProcessorDisplayName)]
public sealed class GameRecordProcessor : ContentProcessor<GameRecordDto, GameRecordContent>
{
    public override GameRecordContent Process(GameRecordDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new GameRecordContent
        {
CellReference = string.IsNullOrWhiteSpace(input.CellReference) ? null : CellReferenceLoader.Load(input.CellReference, context),
};


return content;
    }

    private static void AssertValid(GameRecordDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("GameRecord payload is empty.");
        }

}
}
