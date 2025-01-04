namespace BeeCreak.Game.Scene.Tile;

public struct TileType
{
    public TileType(int id, bool hasVariant)
    {
        Id = id;
        HasVariant = hasVariant;
    }

    public static TileType GrassOnWater => new (0, true);
    public static TileType Water => new (1, false);
    public bool HasVariant { get; set; }
    public int Id { get; set; }
}
