namespace BeeCreak.Generation
{
    using System;
    using System.Collections.Generic;
    using global::BeeCreak.Game.Scene.Tile;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;

    public class ShapeRouter
    {
        private readonly List<RouterCommand> commands;

        private readonly ISprite sprite;

        public ShapeRouter(ISprite sprite)
        {
            this.sprite = sprite;

            Bit = new RouterBit(sprite);
            commands = new List<RouterCommand> { new Circle(7), new Line(5, 3), new Circle(3) };
        }

        private Tile[,] Tiles { get; set; }

        private RouterBit Bit { get; set; }

        public TileMap Route(int size, int complexity = 10, int seed = 0)
        {
            var random = new Random(seed);

            Tiles = new Tile[size, size];

            Bit.SetTiles(Tiles);
            Bit.SetPosition(new Vector2(size / 2, size / 2));
            Bit.SetDirection(Direction.East);

            for (int i = 0; i < complexity; i++)
            {
                if (!Bit.Execute(commands[random.Next(0, commands.Count)]))
                {
                    switch (random.Next(0, 1))
                    {
                        case 0:
                            {
                                Bit.RotateRight();
                                break;
                            }

                        case 1:
                            {
                                Bit.RotateLeft();
                                break;
                            }
                    }
                }
            }

            Flood(size);

            return new TileMap(Tiles);
        }

        private void Flood(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (Tiles[x, y] == null)
                    {
                        Tiles[x, y] = new Tile(sprite, TileType.Forest);
                    }
                }
            }
        }
    }
}