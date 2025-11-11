using System;
using BeeCreak.Content.Pipeline.Extensions.CellRecord;
using BeeCreak.Content.Pipeline.Extensions.EntityReference;
using BeeCreak.Content.Pipeline.Extensions.TileMap;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

[ContentProcessor(DisplayName = "Cell Reference Processor")]
public sealed class CellReferenceProcessor : ContentProcessor<CellReferenceDto, CellReferenceContent>
{
    public override CellReferenceContent Process(CellReferenceDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Cell reference payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Cell reference requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.Base))
        {
            throw new InvalidContentException($"Cell reference '{input.Id}' requires a base cell id.");
        }

        if (string.IsNullOrWhiteSpace(input.TileMap))
        {
            throw new InvalidContentException($"Cell reference '{input.Id}' requires a tile map id.");
        }

        var baseCell = LoadCellRecord(input.Base, context, input.Id);
        var tileMap = LoadTileMap(input.TileMap, context, input.Id);

        var content = new CellReferenceContent
        {
            Id = input.Id,
            BaseCell = baseCell,
            TileMap = tileMap
        };

        if (input.Entities is not null)
        {
            foreach (var entityId in input.Entities)
            {
                if (string.IsNullOrWhiteSpace(entityId))
                {
                    continue;
                }

                var entityReference = LoadEntityReference(entityId, context, input.Id);
                content.EntityReferences.Add(entityReference);
            }
        }

        return content;
    }

    private static CellRecordContent LoadCellRecord(string cellId, ContentProcessorContext context, string referenceId)
    {
        var reference = new ExternalReference<CellRecordContent>($"CellRecord/{cellId}.crec");
        try
        {
            return context.BuildAndLoadAsset<CellRecordContent, CellRecordContent>(reference, "CellRecordProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Cell reference '{referenceId}' failed to load base cell '{cellId}': {ex.Message}", ex);
        }
    }

    private static TileMapContent LoadTileMap(string tileMapId, ContentProcessorContext context, string referenceId)
    {
        var reference = new ExternalReference<TileMapContent>($"TileMap/{tileMapId}.tref");
        try
        {
            return context.BuildAndLoadAsset<TileMapContent, TileMapContent>(reference, "TileMapProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Cell reference '{referenceId}' failed to load tile map '{tileMapId}': {ex.Message}", ex);
        }
    }

    private static EntityReferenceContent LoadEntityReference(string entityReferenceId, ContentProcessorContext context, string referenceId)
    {
        var reference = new ExternalReference<EntityReferenceContent>($"EntityReference/{entityReferenceId}.eref");
        try
        {
            return context.BuildAndLoadAsset<EntityReferenceContent, EntityReferenceContent>(reference, "EntityReferenceProcessor");
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"Cell reference '{referenceId}' failed to load entity reference '{entityReferenceId}': {ex.Message}", ex);
        }
    }
}
