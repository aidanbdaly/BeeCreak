using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = BoundingBoxSheetConfig.ProcessorDisplayName)]
public sealed class BoundingBoxSheetProcessor : ContentProcessor<BoundingBoxSheetDto, BoundingBoxSheetContent>
{
    public override BoundingBoxSheetContent Process(BoundingBoxSheetDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new BoundingBoxSheetContent
        {
Id = input.Id,
};


if (input.BoundingBoxes is not null)
        {
            foreach (var entry in input.BoundingBoxes)
            {
content.BoundingBoxes[entry.Key] = entry.Value;
}
        }
return content;
    }

    private static void AssertValid(BoundingBoxSheetDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("BoundingBoxSheet payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("BoundingBoxSheet requires ''.");
        }

        if (input.BoundingBoxes is null || input.BoundingBoxes.Count < 1)
        {
throw new InvalidContentException("BoundingBoxSheet requires at least 1 '' entries.");
}
}
}
