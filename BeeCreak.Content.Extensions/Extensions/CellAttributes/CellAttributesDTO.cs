public class TileMapDTO
{
    public int Width { get; set; }

    public int Height { get; set; }

    public string[] Tiles { get; set; }
}

public class CellAttributesDTO
{
    public string Tint { get; set; }

    public int LengthOfDay { get; set; }

    public int LengthOfNight { get; set; }

    public TileMapDTO TileMap { get; set; }
}