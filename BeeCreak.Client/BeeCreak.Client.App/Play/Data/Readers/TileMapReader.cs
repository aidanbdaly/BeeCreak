using BeeCreak.Engine.Data.Models;
using BeeCreak.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Readers;

public sealed class TileMapReader : ContentTypeReader<TileMap>
{
    protected override TileMap Read(ContentReader input, TileMap existingInstance)
    {
        string id = input.ReadString();
        SpriteSheet spriteSheet = input.ReadObject<SpriteSheet>();
        BoundingBoxSheet boundingBoxSheet = input.ReadObject<BoundingBoxSheet>();

        var tiles = new Dictionary<Point, string>();

        int rowCount = input.ReadInt32();
        for (int y = 0; y < rowCount; y++)
        {
            int columnCount = input.ReadInt32();
            for (int x = 0; x < columnCount; x++)
            {
                var spriteId = input.ReadString();
                if (string.IsNullOrWhiteSpace(spriteId))
                {
                    continue;
                }

                tiles[new Point(x, y)] = spriteId;
            }
        }

        return new TileMap(id, spriteSheet, boundingBoxSheet, tiles);
    }
}
