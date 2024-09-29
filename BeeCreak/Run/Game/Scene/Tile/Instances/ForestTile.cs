using System;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Tile.Instances;

public class ForestTile : Tile
{
    public ForestTile(IToolCollection tools)
        : base(tools)
    {
        Texture = tools.Static.Sprite.GetTexture("forest");
        Type = TileType.Forest;
    }

    public ForestTile(IToolCollection tools, Vector2 position)
        : base(tools, position)
    {
        Texture = tools.Static.Sprite.GetTexture("forest");
        Type = TileType.Forest;
        Bounds = new Rectangle(
            (int)position.X,
            (int)position.Y,
            tools.Static.TILE_SIZE,
            tools.Static.TILE_SIZE
        );
    }

    public override ForestTileDTO ToDTO()
    {
        return new ForestTileDTO
        {
            Position = Position,
            Type = Type,
            Bounds = Bounds
        };
    }
}
