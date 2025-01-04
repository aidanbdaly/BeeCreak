namespace BeeCreak.Game.Scene
{
    using System;
    using System.Collections.Generic;
    using global::BeeCreak.Config;
    using global::BeeCreak.Game.Scene.Tile;
    using Microsoft.Xna.Framework;

    public class CollisionHandler : ICollisionHandler
    {
        public CollisionHandler(ITileMap tileMap)
        {
            TileMap = tileMap;
        }

        private ITileMap TileMap { get; set; }

        public bool CheckCollision(Vector2 position, Rectangle bounds)
        {
            var x = (int)Math.Round(position.X) / Globals.TileSize;
            var y = (int)Math.Round(position.Y) / Globals.TileSize;

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

                if (tile.Bounds.Intersects(bounds))
                {
                    return true;
                }
            }

            return false;
        }
    }
}