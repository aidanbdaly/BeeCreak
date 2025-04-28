using Microsoft.Xna.Framework;

public class Tile
{
    public string? Type { get; }

    public string? Variant { get; }

    public Rectangle Hitbox { get; }

    public Point Coordinate { get; }

    public Tile(string type, string variant, Rectangle hitbox, Point coordinate)
    {
        Type = type;
        Variant = variant;
        Hitbox = hitbox;
        Coordinate = coordinate;
    }

    public Tile WithType(string type)
    {
        return new Tile(type, Variant, Hitbox, Coordinate);
    }

    public Tile WithVariant(string variant)
    {
        return new Tile(Type, variant, Hitbox, Coordinate);
    }
}