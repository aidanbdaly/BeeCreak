using System.Collections.Generic;
using BeeCreak.App.Game.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.App.Game.Readers;

public sealed class CellReferenceReader : ContentTypeReader<CellReference>
{
    private const string CellRecordAssetRoot = "CellRecord/";
    private const string TileMapAssetRoot = "TileMap/";
    private const string EntityReferenceAssetRoot = "EntityReference/";

    protected override CellReference Read(ContentReader input, CellReference existingInstance)
    {
        string id = input.ReadString();
        string baseCellId = input.ReadString();
        string tileMapId = input.ReadString();

        var baseCell = input.ContentManager.Load<CellRecord>(CellRecordAssetRoot + baseCellId);
        var tileMap = input.ContentManager.Load<TileMapRecord>(TileMapAssetRoot + tileMapId);

        var entities = new List<EntityReference>();
        var cellReference = new CellReference(id, baseCell, entities, tileMap);

        int entityCount = input.ReadInt32();
        for (int i = 0; i < entityCount; i++)
        {
            string entityId = input.ReadString();
            var entity = input.ContentManager.Load<EntityReference>(EntityReferenceAssetRoot + entityId);

            entities.Add(new EntityReference
            {
                Id = entity.Id,
                Base = entity.Base,
                Cell = cellReference,
                Variant = entity.Variant,
                Position = entity.Position
            });
        }

        return cellReference;
    }
}
