namespace BeeCreak.Run;

using System;
using Microsoft.Xna.Framework;

public class WorldGenerator
{
    private readonly Tile[,] TileSet;
    private readonly IContext Context;
    private readonly Random Random;
    private readonly int Size;
    private Vector2 PaintPosition;
    private Direction Direction;
    private Graphic Circle => Graphic.Circle(7);
    private Graphic Line => Graphic.Line(5, 3, Direction);

    public WorldGenerator(IContext context, int seed, int size)
    {
        Context = context;

        Direction = Direction.Right;

        PaintPosition = new Vector2(size / 2, size / 2);

        Random = new Random(seed);

        Size = size;

        TileSet = new Tile[size, size];
    }

    public Tile[,] Generate(int complexity = 30)
    {
        for (int i = 0; i < complexity; i++)
        {
            var feature = Random.Next(0, 3);

            switch (feature)
            {
                case 0:
                {
                    bool paintSuccess = DrawCircle(7);

                    if (!paintSuccess)
                    {
                        Direction = Direction.Next(Direction);
                    }

                    break;
                }
                case 2:
                {
                    bool paintSuccess = DrawLine(5, 3);

                    if (!paintSuccess)
                    {
                        Direction = Direction.Next(Direction);
                    }

                    break;
                }
            }
        }

        Flood();

        return TileSet;
    }

    private bool DrawCircle(int radius)
    {
        var (YFactor, XFactor) = Direction.Value;

        var circleOffsetX = radius * XFactor;
        var circleOffsetY = radius * YFactor;

        var success = DrawShape(
            Circle,
            (int)PaintPosition.X + circleOffsetX,
            (int)PaintPosition.Y + circleOffsetY
        );

        if (!success)
        {
            return false;
        }

        var newDirection = Direction;

        switch (Random.Next(0, 3))
        {
            case 0:
            {
                newDirection = Direction.Next(Direction);
                break;
            }
            case 1:
            {
                newDirection = Direction.Prev(Direction);
                break;
            }
            case 2:
            {
                break;
            }
        }

        var (newYFactor, newXFactor) = newDirection.Value;

        PaintPosition += new Vector2(
            circleOffsetX + (radius + 1) * newXFactor,
            circleOffsetY + (radius + 1) * newYFactor
        );

        Direction = newDirection;

        return true;
    }

    private bool DrawLine(int length, int thickness)
    {
        DrawShape(Line, (int)PaintPosition.X, (int)PaintPosition.Y);

        var (yFactor, xFactor) = Direction.Value;

        PaintPosition += new Vector2((length + 1) * xFactor, (length + 1) * yFactor);

        return true;
    }

    private bool DrawShape(Graphic shape, int positionX, int positionY)
    {
        foreach (var (x, y) in shape.Coordinates)
        {
            var X = x + positionX;
            var Y = y + positionY;

            if (!IsInWorld(X, Y) || TileSet[X, Y] != null)
            {
                return false;
            }
        }
        foreach (var (x, y) in shape.Coordinates)
        {
            var X = x + positionX;
            var Y = y + positionY;

            var texture = Context.Static.SpriteController.GetTexture("grass");
            var position = new Vector2(X * Context.Static.TILE_SIZE, Y * Context.Static.TILE_SIZE);

            TileSet[X, Y] = new Tile(texture, position);
        }

        return true;
    }

    private void Flood()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                if (TileSet[x, y] == null)
                {
                    var texture = Context.Static.SpriteController.GetTexture("forest");
                    var position = new Vector2(
                        x * Context.Static.TILE_SIZE,
                        y * Context.Static.TILE_SIZE
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
