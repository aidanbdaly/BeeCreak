using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.BoundingBoxSheet;

[ContentProcessor(DisplayName = "Bounding Box Sheet Processor")]
public sealed class BoundingBoxSheetProcessor : ContentProcessor<BoundingBoxSheetDto, BoundingBoxSheetContent>
{
    public override BoundingBoxSheetContent Process(BoundingBoxSheetDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Bounding box sheet payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Bounding box sheet requires an id.");
        }

        var content = new BoundingBoxSheetContent
        {
            Id = input.Id
        };

        if (input.BoundingBoxes is null || input.BoundingBoxes.Count == 0)
        {
            throw new InvalidContentException($"Bounding box sheet '{input.Id}' must declare at least one bounding box.");
        }

        foreach (var (key, box) in input.BoundingBoxes)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidContentException($"Bounding box sheet '{input.Id}' contains an empty key.");
            }

            if (box is null)
            {
                throw new InvalidContentException($"Bounding box '{key}' in '{input.Id}' is missing dimensions.");
            }

            var width = box.Width ?? 0;
            var height = box.Height ?? 0;

            content.BoundingBoxes[key] = new BoundingBoxContent
            {
                X = box.X ?? 0,
                Y = box.Y ?? 0,
                Width = width,
                Height = height
            };
        }

        return content;
    }
}
