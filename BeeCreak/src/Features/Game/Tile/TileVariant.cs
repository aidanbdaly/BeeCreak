namespace BeeCreak.Game.Scene.Tile;

public struct TileVariant
{
    public TileVariant(int id)
    {
        Id = id;
    }

    public static TileVariant Left => new (0);
    public static TileVariant Top => new (1);
    public static TileVariant Right => new (2);
    public static TileVariant Bottom => new (3);
    public static TileVariant TopLeftInner => new (4);
    public static TileVariant TopRightInner => new (5);
    public static TileVariant BottomRightInner => new (6);
    public static TileVariant BottomLeftInner => new (7);
    public static TileVariant TopLeftOuter => new (8);
    public static TileVariant TopRightOuter => new (9);
    public static TileVariant BottomRightOuter => new (10);
    public static TileVariant BottomLeftOuter => new (11);
    public static TileVariant Center => new (12);

    public int Id { get; set; }
}