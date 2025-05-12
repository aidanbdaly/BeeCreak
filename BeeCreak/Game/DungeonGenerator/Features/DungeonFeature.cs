using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

// public interface DungeonFeature
// {
//     /// <summary>
//     /// The shape of the feature.
//     /// </summary>
//     List<Point> Shape { get; }

//     /// <summary>
//     /// The nodes of the feature.
//     /// These are the points that will be used to connect the feature to other features.
//     /// </summary>
//     List<Point> Connectors { get; }
// }

public abstract class DungeonFeature
{
    /// <summary>
    /// The shape of the feature.
    /// </summary>
    public abstract List<Point> Shape { get; }

    /// <summary>
    /// The nodes of the feature.
    /// These are the points that will be used to connect the feature to other features.
    /// </summary>
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
}