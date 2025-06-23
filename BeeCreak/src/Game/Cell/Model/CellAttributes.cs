using Microsoft.Xna.Framework;

public class CellAttributes
{
    public CellAttributes(Color tint, int lengthOfDay, int lengthOfNight, Grid<TileState> tileMap)
    {
        Tint = tint;
        LengthOfDay = lengthOfDay;
        LengthOfNight = lengthOfNight;
        TileMap = tileMap;
    }

    public Color Tint { get; }

    public int LengthOfDay { get; }

    public int LengthOfNight { get; }

    public Grid<TileState> TileMap { get; }

    public List<EntityState> Entities { get; }
}