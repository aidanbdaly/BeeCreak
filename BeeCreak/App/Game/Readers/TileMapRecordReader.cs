using System.Collections.Generic;
using BeeCreak.App.Game.Models;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.App.Game.Readers;

public sealed class TileMapRecordReader : ContentTypeReader<TileMapRecord>
{
    protected override TileMapRecord Read(ContentReader input, TileMapRecord existingInstance)
    {
        string id = input.ReadString();
        SpriteSheet spriteSheet = input.ReadObject<SpriteSheet>();

        int tileCount = input.ReadInt32();
        var tiles = new Dictionary<Point, string>(tileCount);

        for (int i = 0; i < tileCount; i++)
        {
            int x = input.ReadInt32();
            int y = input.ReadInt32();
            string spriteId = input.ReadString();
            tiles[new Point(x, y)] = spriteId;
        }

        return new TileMapRecord(id, spriteSheet, tiles);
    }
}
