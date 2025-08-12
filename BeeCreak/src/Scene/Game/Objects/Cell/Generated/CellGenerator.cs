using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

public class DungeonGenerator
{
    private readonly List<DungeonFeature> features;

    private readonly Random random = new();

    public DungeonGenerator(List<DungeonFeature> features)
    {
        this.features = features;
    }

    public Point Position { get; set; }

    public TileState[,] TileMap { get; set; }

    public TileState[,] Route(int size, int complexity = 10, int seed = 0)
    {
        TileMap = new TileState[size, size];
        Position = new Point(size / 2, size / 2);

        for (int i = 0; i < complexity; i++)
        {
            Lay(features[random.Next(0, features.Count)]);
        }

        return TileMap;
    }

    private void Lay(DungeonFeature original)
    {
        Point? chosenConnector = null;
        List<Point> chosenShape = null;

        for (int rot = 0; rot < 4 && chosenConnector == null; rot++)
        {
            original.RotateClockwise();

            foreach (var c in original.Connectors)
            {
                bool fits = true;
                foreach (var p in original.Shape)
                {
                    var tp = p + Position - c;

                    if (tp.X < 0 || tp.Y < 0 ||
                        tp.X >= TileMap.GetLength(0) ||
                        tp.Y >= TileMap.GetLength(1) ||
                        TileMap[tp.X, tp.Y] != null)
                    {
                        fits = false; break;
                    }
                }

                if (fits)
                {
                    chosenConnector = c;
                    chosenShape = [.. original.Shape];
                    break;
                }
            }
        }

        if (chosenConnector == null)
        {
            return;
        }

        foreach (var p in chosenShape!)
        {
            var tp = p + Position - chosenConnector.Value;

            TileMap[tp.X, tp.Y] = new TileState { ContentId = "Default" };
        }

        var exits = original.Connectors
                            .Where(c => c != chosenConnector.Value)
                            .ToList();

        if (exits.Count > 0)
        {
            var next = exits[random.Next(exits.Count)];

            Position -= chosenConnector.Value;
            Position += next + Vector2.Normalize(next.ToVector2()).ToPoint();
        }
    }
}