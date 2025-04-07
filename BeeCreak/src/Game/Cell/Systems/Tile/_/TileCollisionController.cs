using System;
using System.Collections.Generic;
using BeeCreak.Shared.Data.Config;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class TileCollisionController : ITileCollisionController
{
    private readonly ITile[,] tiles;

    public TileCollisionController(ITile[,] tiles)
    {
        this.tiles = tiles;
    }

    public bool CanMoveBy(Vector2 amount, Rectangle boundingBox)
    {
        var position = new Vector2(boundingBox.X + amount.X, boundingBox.Y + amount.Y);

        var x = (int)Math.Round(position.X) / GlobalConstants.TILE_RESOLUTION;
        var y = (int)Math.Round(position.Y) / GlobalConstants.TILE_RESOLUTION;

        var adjacentTilePositions = new List<(int, int)>
            {
                (x, y),
                (x, y + 1),
                (x, y - 1),
                (x + 1, y),
                (x - 1, y),
                (x + 1, y + 1),
                (x - 1, y - 1),
                (x + 1, y - 1),
                (x - 1, y + 1),
            };

        foreach (var (adjacentTileX, adjacentTileY) in adjacentTilePositions)
        {
            var tile = tiles[adjacentTileX, adjacentTileY];

            // if (tile.Bounds.Intersects(boundingBox))
            // {
            //     return true;
            // }
        }

        return false;
    }
};