using System;
using System.Collections.Generic;
using BeeCreak.Shared.Data.Config;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class CollisionHandler : ICollisionHandler
{
    public CollisionHandler()
    {
    }

    private ITileMap TileMap { get; set; }

    private Rectangle BoundingBox { get; set; }

    public void SetTileMap(ITileMap tileMap)
    {
        TileMap = tileMap;
    }

    public void SetBoundingBox(Rectangle boundingBox)
    {
        BoundingBox = boundingBox;
    }

    public bool CanMoveBy(Vector2 amount)
    {
        var position = new Vector2(BoundingBox.X + amount.X, BoundingBox.Y + amount.Y);

        var x = (int)Math.Round(position.X) / Globals.TILE_SIZE;
        var y = (int)Math.Round(position.Y) / Globals.TILE_SIZE;

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
            var tile = TileMap.GetTile(adjacentTileX, adjacentTileY);

            if (tile.Bounds.Intersects(BoundingBox))
            {
                return true;
            }
        }

        return false;
    }
};