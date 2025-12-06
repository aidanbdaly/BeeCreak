using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = CellReferenceConfig.ProcessorDisplayName)]
public sealed class CellReferenceProcessor : ContentProcessor<CellReferenceDto, CellReferenceContent>
{
    public override CellReferenceContent Process(CellReferenceDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new CellReferenceContent
        {
Id = input.Id,
CellRecord = string.IsNullOrWhiteSpace(input.CellRecord) ? null : CellRecordLoader.Load(input.CellRecord, context),
TileMap = string.IsNullOrWhiteSpace(input.TileMap) ? null : TileMapLoader.Load(input.TileMap, context),
};


if (input.Entities is not null)
        {
            foreach (var item in input.Entities)
            {
content.Entities.Add(item ?? string.Empty);
}
        }
return content;
    }

    private static void AssertValid(CellReferenceDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("CellReference payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("CellReference requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.TileMap))
        {
            throw new InvalidContentException("CellReference requires ''.");
        }

}
}
