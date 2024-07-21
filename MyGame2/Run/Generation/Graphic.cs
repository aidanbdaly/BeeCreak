namespace BeeCreak.Run;

using System.Collections.Generic;
public struct Graphic
{
    public List<(int, int)> Coordinates { get; set; }

    private Graphic(List<(int, int)> coordinates)
    {
        Coordinates = coordinates;
    }

    public static Graphic Circle(int radius)
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

        return new Graphic(coordinates);
    }

    public static Graphic Line(int length, int thickness, Direction direction)
    {
        var coordinates = new List<(int, int)>();

        var (yFactor, xFactor) = direction.Value;

        for (int i = 0; i < length + 1; i++)
        {
            for (int j = -thickness / 2; j < thickness / 2 + 1; j++)
            {
                var X = i * xFactor + j * yFactor;
                var Y = i * yFactor + j * xFactor;

                coordinates.Add((X, Y));
            }
        }

        return new Graphic(coordinates);
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
}
