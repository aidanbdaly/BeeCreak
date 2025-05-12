using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

public class DungeonGenerator
{
    private readonly List<DungeonFeature> features;

    public DungeonGenerator(List<DungeonFeature> features)
    {
        this.features = features;
    }

    public int[,] Route(int size, int complexity = 10, int seed = 0)
    {
        var context = new GeneratorContext(size, seed);

        for (int i = 0; i < complexity; i++)
        {
            Lay(context, features[context.Random.Next(0, features.Count)]);
        }

        return context.Tiles;
    }


    private void Lay(GeneratorContext context, DungeonFeature feature)
    {
        Point selectedConnector = Point.Zero;
        List<Point>? selectedShape = null;

        for (int i = 0; i < 4; i++) 
        {
            foreach (var connector in feature.Connectors)
            {
                bool valid = true;

                foreach (var point in feature.Shape)
                {
                    var translatedPoint = point + context.Position - connector;

                    if (translatedPoint.X < 0 || translatedPoint.Y < 0 ||
                        translatedPoint.X >= context.Tiles.GetLength(0) ||
                        translatedPoint.Y >= context.Tiles.GetLength(1) ||
                        context.Tiles[translatedPoint.X, translatedPoint.Y] != 0)
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    selectedConnector = connector;
                    selectedShape = feature.Shape.ToList();
                    goto FoundValid; 
                }
            }

            feature.RotateClockwise();
        }

    FoundValid:
        if (selectedConnector == Point.Zero || selectedShape == null)
            return; 

        foreach (var point in selectedShape)
        {
            var translatedPoint = point + context.Position - selectedConnector;
            context.Tiles[translatedPoint.X, translatedPoint.Y] = 1;
        }

        var nextConnector = feature.Connectors
            .Where(c => c != selectedConnector)
            .OrderBy(_ => context.Random.Next())
            .FirstOrDefault();

        if (nextConnector != default)
        {
            context.Position -= selectedConnector;
            context.Position += nextConnector;
        }
    }
}