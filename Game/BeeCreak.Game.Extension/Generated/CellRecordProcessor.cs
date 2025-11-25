using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = CellRecordConfig.ProcessorDisplayName)]
public sealed class CellRecordProcessor : ContentProcessor<CellRecordDto, CellRecordContent>
{
    public override CellRecordContent Process(CellRecordDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new CellRecordContent
        {
Id = input.Id,
};


return content;
    }

    private static void AssertValid(CellRecordDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("CellRecord payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("CellRecord requires ''.");
        }

}
}
