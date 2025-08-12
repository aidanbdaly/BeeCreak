using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models;

public class CellBlueprintReader : ContentTypeReader<CellBlueprint>
{
    protected override CellBlueprint Read(ContentReader input, CellBlueprint existingInstance)
    {
        // Read tile map dimensions
        int width = input.ReadInt32();
        int height = input.ReadInt32();
        
        // Read tile map data
        int tileCount = input.ReadInt32();
        TileState[] tileData = new TileState[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            string tileString = input.ReadString();
            tileData[i] = new TileState { ContentId = tileString };
        }
        
        var tileMap = new ReadOnlyGrid<TileState>(width, height, tileData);
        
        // Read entities
        int entityCount = input.ReadInt32();
        EntityState[] entities = new EntityState[entityCount];
        for (int i = 0; i < entityCount; i++)
        {
            string entityString = input.ReadString();
            entities[i] = new EntityState 
            { 
                ContentId = entityString,
                Variant = "facing_west",
                Position = new Vector2(32 * 10, 32 * 10)
            };
        }
        
        return new CellBlueprint(entities, tileMap);
    }
}
