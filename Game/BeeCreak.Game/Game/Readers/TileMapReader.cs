using BeeCreak.Engine.Data.Models;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Game.Readers;

public sealed class TileMapReader : ContentTypeReader<TileMap>
{
    protected override TileMap Read(ContentReader input, TileMap existingInstance)
    {
        string id = input.ReadString();
        SpriteSheet spriteSheet = input.ReadObject<SpriteSheet>();
        BoundingBoxSheet boundingBoxSheet = input.ReadObject<BoundingBoxSheet>();

        int tileCount = input.ReadInt32();

        var tiles = new Dictionary<Point, string>();

        for (int i = 0; i < tileCount; i++)
        {
            int x = input.ReadInt32();
            int y = input.ReadInt32();
            string spriteId = input.ReadString();
            tiles[new Point(x, y)] = spriteId;
        }

        return new TileMap(id, spriteSheet, boundingBoxSheet, tiles);
    }
}
