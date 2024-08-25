using System.Collections.Generic;
using System.Linq;
using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Generation;

public enum ShapeType
{
    Circle,
    Line
}

public struct Shape
{
    public List<(int, int)> Coordinates { get; set; }

    private Shape(List<(int, int)> coordinates)
    {
        Coordinates = coordinates;
    }

    public static Shape Circle(int radius)
    {
        var coordinates = new List<(int, int)>();

        int x = radius;
        int y = 0;
        int decision = 1 - radius;

        var centerX = 0 + radius;

        while (x >= y)
        {
            // Draw horizontal lines between the symmetrical points
            DrawHorizontalLine(-x, x, y).ForEach(coordinates.Add);
            DrawHorizontalLine(-y, y, x).ForEach(coordinates.Add);
            DrawHorizontalLine(-x, x, -y).ForEach(coordinates.Add);
            DrawHorizontalLine(-y, y, -x).ForEach(coordinates.Add);

            y++;

            if (decision <= 0)
            {
                decision += 2 * y + 1;
            }
            else
            {
                x--;
                decision += 2 * (y - x) + 1;
            }
        }

        return new Shape(coordinates);
    }

    public static Shape Line(int length, int thickness)
    {
        var coordinates = new List<(int, int)>();

        for (int i = -thickness / 2; i < thickness / 2 + 1; i++)
        {
            coordinates.AddRange(DrawHorizontalLine(-length / 2, length / 2, i));
        }

        return new Shape(coordinates);
    }

    public static List<(int, int)> Rotate(List<(int, int)> coordinates, Direction direction)
    {
        return coordinates.Select(coordinate => RotateCoordinate(coordinate, direction)).ToList();
    }

    private static List<(int, int)> DrawHorizontalLine(int startX, int endX, int y)
    {
        var coordinates = new List<(int, int)>();

        for (int i = startX; i < endX + 1; i++)
        {
            coordinates.Add((i, y));
        }

        return coordinates;
    }

    private static (int, int) RotateCoordinate((int, int) coordinate, Direction direction)
    {
        int x = coordinate.Item1;
        int y = coordinate.Item2;

        return direction.Type switch
        {
            DirectionType.East => (x, y), // 0 degrees or no rotation
            DirectionType.South => (y, -x), // 90 degrees clockwise
            DirectionType.West => (-x, -y), // 180 degrees
            DirectionType.North => (-y, x), // 270 degrees clockwise (90 degrees counterclockwise)
            _ => (x, y),
        };
    }
}
