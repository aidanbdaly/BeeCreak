namespace BeeCreak.Game.Scene
{
    using System;
    using System.Collections.Generic;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public class CollisionHandler : ICollisionHandler
    {
        public CollisionHandler(IToolCollection tools, ITile[,] tiles)
        {
            Tools = tools;
            Tiles = tiles;
        }

        private ITile[,] Tiles { get; set; }

        private IToolCollection Tools { get; set; }

        public bool CheckCollision(Vector2 position, Rectangle bounds)
        {
            var X = (int)Math.Round(position.X) / Tools.Static.TILE_SIZE;
            var Y = (int)Math.Round(position.Y) / Tools.Static.TILE_SIZE;

            var tilesToTestForIntersection = new List<(int, int)>
            {
                (X, Y),
                (X, Y + 1),
                (X, Y - 1),
                (X + 1, Y),
                (X - 1, Y),
                (X + 1, Y + 1),
                (X - 1, Y - 1),
                (X + 1, Y - 1),
                (X - 1, Y + 1),
            };

            foreach (var (x, y) in tilesToTestForIntersection)
            {
                var tile = Tiles[x, y];

                if (tile.Bounds.Intersects(bounds))
                {
                    return true;
                }
            }

            return false;
        }
    }
}