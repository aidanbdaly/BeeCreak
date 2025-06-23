using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

public abstract class DungeonFeature
{
    public abstract List<Point> Shape { get; }

    public abstract List<Point> Connectors { get; }

    public void RotateClockwise()
    {
        for (int i = 0; i < Shape.Count; i++)
        {
            var point = Shape[i];

            Shape[i] = new Point(point.Y, -point.X);
        }

        for (int i = 0; i < Connectors.Count; i++)
        {
            var point = Connectors[i];

            Connectors[i] = new Point(point.Y, -point.X);
        }
    }

    public abstract DungeonFeature Clone();
}