using System;
using System.Collections.Generic;
using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

public class ShapeRouter : IShapeRouter
{
    private readonly List<RouterCommand> commands;

    public ShapeRouter()
    {
        Router = new RouterBit();
        commands = new List<RouterCommand> { new Circle(7), new Line(5, 3), new Circle(3) };
    }

    private ITile[,] Tiles { get; set; }

    private RouterBit Router { get; set; }

    public ITile[,] Route(int size, int complexity = 10, int seed = 0)
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

        return Tiles;
    }

    private void Flood(int size)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (Tiles[x, y] == null)
                {
                    var position = new Vector2(x * GlobalConstants.TILE_RESOLUTION, y * GlobalConstants.TILE_RESOLUTION);

                    var tile = new Tile()
                    {
                        Type = TileAssetType.Water,
                        Variant = TileAssetVariant.Default,
                        Position = position
                    };

                    Tiles[x, y] = tile;
                }
            }
        }
    }
}