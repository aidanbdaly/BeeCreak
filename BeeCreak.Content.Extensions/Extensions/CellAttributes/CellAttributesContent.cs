using Microsoft.Xna.Framework;

public class TileMapContent
{
    public int Width { get; set; }

    public int Height { get; set; }

    public string[] Tiles { get; set; }
}

public class CellAttributesContent
{
    public Color Tint { get; set; }

    public int LengthOfDay { get; set; }

    public int LengthOfNight { get; set; }

    public TileMapContent TileMap { get; set; }
}