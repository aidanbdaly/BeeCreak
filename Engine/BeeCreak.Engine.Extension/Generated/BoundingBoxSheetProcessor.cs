using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "BoundingBoxSheet Processor")]
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
content.BoundingBoxes[entry.Key] = MapBoundingBoxesEntry(entry.Value, context);
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

private static BoundingBoxSheetContent.BoundingBoxesEntryContent MapBoundingBoxesEntry(BoundingBoxSheetDto.BoundingBoxesEntryDto? input, ContentProcessorContext context)
    {
        if (input is null)
        {
            return new BoundingBoxSheetContent.BoundingBoxesEntryContent();
        }

        var content = new BoundingBoxSheetContent.BoundingBoxesEntryContent();
content.X = input.X;
content.Y = input.Y;
content.W = input.W;
content.H = input.H;
return content;
    }

private static TContent LoadAsset<TContent>(
        string assetId,
        string assetName,
        string directory,
        string extension,
        string processor,
        ContentProcessorContext context)
    {
        if (string.IsNullOrWhiteSpace(assetId))
        {
            throw new InvalidContentException($"{assetName} reference is empty.");
        }

        var assetPath = string.Concat(directory, "/", assetId, extension);
        var reference = new ExternalReference<TContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<TContent, TContent>(reference, processor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"{assetName} '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
