using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "CellRecord Processor")]
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
