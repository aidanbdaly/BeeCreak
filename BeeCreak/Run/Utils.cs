namespace BeeCreak.Run;

using System;
using Microsoft.Xna.Framework;

public static class Utils
{
    public static Vector2 Clamp(Vector2 position, Vector2 maxPosition)
    {
        var x = Math.Max(0, Math.Min(maxPosition.X, position.X));
        var y = Math.Max(0, Math.Min(maxPosition.Y, position.Y));

        return new Vector2(x, y);
    }

    public static int Quotient(int a, int b)
    {
        return (a - (a % b)) / b;
    }
}
