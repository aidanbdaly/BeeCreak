namespace BeeCreak.Shared.Services.Static;

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public struct Stencil
{
    public static List<Point> Circle(int radius, Point center = default)
    {
        if (radius < 0) throw new ArgumentOutOfRangeException(nameof(radius));

        int rows = radius * 2 + 1;
        int[] minX = Enumerable.Repeat(int.MaxValue, rows).ToArray();
        int[] maxX = Enumerable.Repeat(int.MinValue, rows).ToArray();

        void Stamp(int dx, int dy)             
        {
            void mark(int x, int y)
            {
                int row = y + radius;           
                if (x < minX[row]) minX[row] = x;
                if (x > maxX[row]) maxX[row] = x;
            }

            mark(dx, dy);
            mark(-dx, dy);
            mark(dx, -dy);
            mark(-dx, -dy);
            mark(dy, dx);
            mark(-dy, dx);
            mark(dy, -dx);
            mark(-dy, -dx);
        }

        int x = 0, y = radius;
        int d = 3 - 2 * radius;

        while (y >= x)
        {
            Stamp(x, y);
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

        var pts = new List<Point>(rows * rows);   
        for (int row = 0; row < rows; row++)
        {
            int lx = minX[row];
            int rx = maxX[row];

            if (lx == int.MaxValue) continue;    

            for (int xCol = lx; xCol <= rx; xCol++)
                pts.Add(new Point(center.X + xCol, center.Y + (row - radius)));
        }

        return pts;
    }
    
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
