using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "GameRecord Processor")]
public sealed class GameRecordProcessor : ContentProcessor<GameRecordDto, GameRecordContent>
{
    public override GameRecordContent Process(GameRecordDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new GameRecordContent
        {
CellReference = string.IsNullOrWhiteSpace(input.CellReference) ? null : LoadAsset<CellReferenceContent>(input.CellReference, "CellReference", "CellReference", ".cref", "CellReferenceProcessor", context),
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
