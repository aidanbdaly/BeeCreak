using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "CellReference Processor")]
public sealed class CellReferenceProcessor : ContentProcessor<CellReferenceDto, CellReferenceContent>
{
    public override CellReferenceContent Process(CellReferenceDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new CellReferenceContent
        {
Id = input.Id,
CellRecord = string.IsNullOrWhiteSpace(input.CellRecord) ? null : LoadAsset<CellRecordContent>(input.CellRecord, "CellRecord", "CellRecord", ".crec", "CellRecordProcessor", context),
TileMap = string.IsNullOrWhiteSpace(input.TileMap) ? null : LoadAsset<TileMapContent>(input.TileMap, "TileMap", "TileMap", ".tref", "TileMapProcessor", context),
};


if (input.EntityReferenceArray is not null)
        {
            foreach (var item in input.EntityReferenceArray)
            {
if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
content.EntityReferenceArray.Add(LoadAsset<EntityReferenceContent>(item, "EntityReference", "EntityReference", ".eref", "EntityReferenceProcessor", context));
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
