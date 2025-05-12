namespace BeeCreak.Shared.Services.Static;

using System;
using Microsoft.Xna.Framework;

public class CircleFeature : DungeonFeature
{
    public CircleFeature(int radius)
    {
        Radius = radius;
    }
    
    private int Radius { get; set; }

    public override List<Point> Shape => Stencil.Circle(Radius);

    public override List<Point> Connectors => new()
    {
        Shape.FindAll(p => p.Y == 0).OrderBy(p => p.X).ToList()[0],
        Shape.FindAll(p => p.Y == 0).OrderByDescending(p => p.X).ToList()[0],
        Shape.FindAll(p => p.X == 0).OrderBy(p => p.Y).ToList()[0],
        Shape.FindAll(p => p.X == 0).OrderByDescending(p => p.Y).ToList()[0],
    };
}