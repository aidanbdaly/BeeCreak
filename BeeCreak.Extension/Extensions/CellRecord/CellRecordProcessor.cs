using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.CellRecord;

[ContentProcessor(DisplayName = "Cell Record Processor")]
public sealed class CellRecordProcessor : ContentProcessor<CellRecordDto, CellRecordContent>
{
    public override CellRecordContent Process(CellRecordDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Cell record payload is empty.");
        }

        return new CellRecordContent { Id = input.Id };
    }
}
