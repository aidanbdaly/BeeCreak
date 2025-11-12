using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Models;

public readonly record struct ImmutableRectangle(int X, int Y, int Width, int Height)
{
    public Rectangle ToRectangle() => new Rectangle(X, Y, Width, Height);

    public static ImmutableRectangle FromRectangle(Rectangle rectangle) =>
        new ImmutableRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
}
