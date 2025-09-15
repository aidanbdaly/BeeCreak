namespace BeeCreak.Shared.Services.Static;

using Microsoft.Xna.Framework;

public class RectangleFeature : DungeonFeature
{
    public RectangleFeature(int length, int width)
    {
        Length = length;
        Width = width;
    }

    public int Length { get; set; }

    public int Width { get; set; }

    public override List<Point> Shape => Stencil.Rectangle(Length, Width);

    public override List<Point> Connectors =>
    [
        Shape.FindAll(p => p.Y == 0).OrderBy(p => p.X).ToList()[0],
        Shape.FindAll(p => p.Y == 0).OrderByDescending(p => p.X).ToList()[0],
        Shape.FindAll(p => p.X == 0).OrderBy(p => p.Y).ToList()[0],
        Shape.FindAll(p => p.X == 0).OrderByDescending(p => p.Y).ToList()[0],
    ];
    
    public override DungeonFeature Clone()
    {
        return new RectangleFeature(Length, Width);
    }
}

