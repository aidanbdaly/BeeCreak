using System;
using System.Collections.Generic;
using BeeCreak.Config;
using BeeCreak.Scene.Main.Scene.Tile;
using BeeCreak.Tools;
using BeeCreak.Utilities.Static;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

public class ShapeRouter : IShapeRouter
{
    private readonly List<RouterCommand> commands;

    private readonly IServiceProvider serviceProvider;

    public ShapeRouter(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        Router = new RouterBit(serviceProvider);
        commands = new List<RouterCommand> { new Circle(7), new Line(5, 3), new Circle(3) };
    }

    private ITile[,] Tiles { get; set; }

    private RouterBit Router { get; set; }

    public ITileMap Route(int size, int complexity = 10, int seed = 0)
    {
        var random = new Random(seed);

        Tiles = new Tile[size, size];

        Router.SetTiles(Tiles);
        Router.SetPosition(new Vector2(size / 2, size / 2));
        Router.SetDirection(Direction.East);

        for (int i = 0; i < complexity; i++)
        {
            if (!Router.Execute(commands[random.Next(0, commands.Count)]))
            {
                switch (random.Next(0, 1))
                {
                    case 0:
                        {
                            Router.RotateRight();
                            break;
                        }

                    case 1:
                        {
                            Router.RotateLeft();
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
                    var position = new Vector2(x * Globals.TILE_SIZE, y * Globals.TILE_SIZE);

                    var tile = serviceProvider.GetRequiredService<ITile>();

                    tile.SetType(TileType.Water);
                    tile.SetVariant(TileVariant.Default);
                    tile.SetPosition(position);

                    Tiles[x, y] = tile;
                }
            }
        }
    }
}