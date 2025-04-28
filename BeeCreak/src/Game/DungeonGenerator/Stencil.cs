namespace BeeCreak.Shared.Services.Static;

using System.Collections.Generic;
using Microsoft.Xna.Framework;

/// <summary>
/// This class contains methods to generate 2D stencils for various shapes.
/// All stencils are centered around the origin (0, 0).
/// </summary>
public struct Stencil
{
    /// <summary>
    /// Generates a list of coordinates that form a circle with the given radius.
    /// Correct implementation using Bresenham's circle algorithm.
    /// </summary>
    public static List<Point> Circle(int radius)
    {
        var coordinates = new List<Point>();

        int x = 0;
        int y = radius;
        int d = 3 - 2 * radius;

        void PlotCirclePoints(int centerX, int centerY, int x, int y)
        {
            coordinates.Add(new Point(centerX + x, centerY + y));
            coordinates.Add(new Point(centerX - x, centerY + y));
            coordinates.Add(new Point(centerX + x, centerY - y));
            coordinates.Add(new Point(centerX - x, centerY - y));
            coordinates.Add(new Point(centerX + y, centerY + x));
            coordinates.Add(new Point(centerX - y, centerY + x));
            coordinates.Add(new Point(centerX + y, centerY - x));
            coordinates.Add(new Point(centerX - y, centerY - x));
        }

        while (y >= x)
        {
            PlotCirclePoints(0, 0, x, y);
            x++;

            if (d > 0)
            {
                y--;
                d = d + 4 * (x - y) + 10;
            }
            else
            {
                d = d + 4 * x + 6;
            }
        }

        return coordinates;
    }

    /// <summary>
    /// Generates a list of coordinates that form a rectangle.
    /// </summary>
    public static List<Point> Rectangle(int length, int thickness)
    {
        var coordinates = new List<Point>();

        for (int i = -thickness / 2; i <= thickness / 2; i++)
        {
            for (int j = -length / 2; j <= length / 2; j++)
            {
                coordinates.Add(new Point(j, i));
            }
        }

        return coordinates;
    }
}
