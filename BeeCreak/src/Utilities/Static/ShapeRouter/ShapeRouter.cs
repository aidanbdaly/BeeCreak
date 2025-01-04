using System;
using System.Collections.Generic;
using BeeCreak.Config;
using BeeCreak.Game.Scene.Tile;
using BeeCreak.Tools;
using BeeCreak.Utilities.Static;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Generation;

public class ShapeRouter : IShapeRouter
{
    private readonly List<RouterCommand> commands;

    private readonly IServiceProvider serviceProvider;

    public ShapeRouter(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        Bit = new RouterBit();
        commands = new List<RouterCommand> { new Circle(7), new Line(5, 3), new Circle(3) };
    }

    private Tile[,] Tiles { get; set; }

    private RouterBit Bit { get; set; }

    public ITileMap Route(int size, int complexity = 10, int seed = 0)
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

        var tileMap = serviceProvider.GetRequiredService<ITileMap>();

        tileMap.SetTiles(Tiles);
        tileMap.RecalculateTileVariants();
        tileMap.Bake();

        return tileMap;
    }

    private void Flood(int size)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (Tiles[x, y] == null)
                {
                    var bounds = new Rectangle(x * Globals.TileSize, y * Globals.TileSize, Globals.TileSize, Globals.TileSize);
                    var position = new Vector2(x * Globals.TileSize, y * Globals.TileSize);

                    var tile = new Tile(TileType.Water, TileVariant.Center, position, bounds);

                    Tiles[x, y] = tile;
                }
            }
        }
    }
}