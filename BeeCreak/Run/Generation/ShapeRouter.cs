using System;
using System.Collections.Generic;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Generation;

public class ShapeRouter
{
    private readonly Tile[,] TileSet;
    private readonly IToolCollection Tools;
    private readonly Random Random;
    private readonly int Size;
    private RouterBit Bit;
    private readonly List<RouterCommand> Commands;

    public ShapeRouter(IToolCollection tools, int seed, int size)
    {
        Tools = tools;

        Bit = new RouterBit(new Vector2(size / 2, size / 2), Direction.Right);

        Commands = new List<RouterCommand> { new Circle(7), new Line(5, 3), new Circle(3) };

        Random = new Random(seed);

        Size = size;

        TileSet = new Tile[size, size];
    }

    public Tile[,] Route(int complexity = 10)
    {
        for (int i = 0; i < complexity; i++)
        {
            var command = Commands[Random.Next(0, Commands.Count)];

            var success = Execute(command, Tools.Static.Sprite.GetTexture("grass"));

            if (!success)
            {
                switch (Random.Next(0, 1))
                {
                    case 0:
                    {
                        Bit.Direction = Direction.Next(Bit.Direction);
                        break;
                    }
                    case 1:
                    {
                        Bit.Direction = Direction.Prev(Bit.Direction);
                        break;
                    }
                }
            }
        }

        Flood(Tools.Static.Sprite.GetTexture("forest"));

        return TileSet;
    }

    private bool Execute(RouterCommand command, Texture2D texture)
    {
        var shapeCoordinates = command.Shape.Coordinates;

        if (!command.IsSymmetric)
        {
            shapeCoordinates = Shape.Rotate(shapeCoordinates, Bit.Direction);
        }

        foreach (var (x, y) in shapeCoordinates)
        {
            var X =
                x + (int)Bit.Position.X + command.Offset * (int)Bit.Direction.Value.X;
            var Y =
                y + (int)Bit.Position.Y + command.Offset * (int)Bit.Direction.Value.Y;

            if (!IsInWorld(X, Y) || TileSet[X, Y] != null)
            {
                return false;
            }
        }

        foreach (var (x, y) in shapeCoordinates)
        {
            var X =
                x + (int)Bit.Position.X + command.Offset * (int)Bit.Direction.Value.X;
            var Y =
                y + (int)Bit.Position.Y + command.Offset * (int)Bit.Direction.Value.Y;

            var position = new Vector2(X * Tools.Static.TILE_SIZE, Y * Tools.Static.TILE_SIZE);

            TileSet[X, Y] = new Tile(texture, position);
        }

        Bit = command.MoveRouterBit(Bit);

        return true;
    }

    private void Flood(Texture2D texture)
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                if (TileSet[x, y] == null)
                {
                    var position = new Vector2(
                        x * Tools.Static.TILE_SIZE,
                        y * Tools.Static.TILE_SIZE
                    );

                    TileSet[x, y] = new Tile(texture, position, true);
                }
            }
        }
    }

    public bool IsInWorld(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Size && y < Size;
    }
}