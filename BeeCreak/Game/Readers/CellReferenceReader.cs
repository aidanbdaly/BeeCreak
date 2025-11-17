using System.Collections.Generic;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Game.Readers;

public sealed class CellReferenceReader : ContentTypeReader<CellReference>
{
    protected override CellReference Read(ContentReader input, CellReference existingInstance)
    {
        string id = input.ReadString();
        var baseCell = input.ReadObject<CellRecord>();
        var tileMap = input.ReadObject<TileMap>();

        var entities = new List<EntityReference>();
        var cellReference = new CellReference(id, baseCell, entities, tileMap);

        int entityCount = input.ReadInt32();
        for (int i = 0; i < entityCount; i++)
        {
            var entity = input.ReadObject<EntityReference>();
            entity.Cell = cellReference;
            entities.Add(entity);
        }

        return cellReference;
    }
}
