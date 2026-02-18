using BeeCreak.Models;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Readers;

public sealed class CellReferenceReader : ContentTypeReader<CellReference>
{
    protected override CellReference Read(ContentReader input, CellReference existingInstance)
    {
        string id = input.ReadString();
        
        var cellRecord = input.ReadObject<CellRecord>();

        var entities = new List<EntityReference>();
        int entityCount = input.ReadInt32();
        for (int i = 0; i < entityCount; i++)
        {
            var entity = input.ReadObject<EntityReference>();
            entities.Add(entity);
        }

        var tileMap = input.ReadObject<TileMap>();

        return new CellReference(id, cellRecord, entities, tileMap);
    }
}
