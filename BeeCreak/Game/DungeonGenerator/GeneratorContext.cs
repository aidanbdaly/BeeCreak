using Microsoft.Xna.Framework;

public class GeneratorContext
{
    public GeneratorContext(int size, int seed = 0)
    {
        Random = new Random(seed);
        Size = size;
        Tiles = new int[size, size];
    }

    public int[,] Tiles { get; set; }

    public int Size { get; set; }

    public Random Random { get; set; } = new Random();

    public Point Position { get; set; } = new Point(0, 0);
}