namespace BeeCreak.Content.Pipeline.Extensions;

public class BlueprintTileMapDTO
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string[] Data { get; set; }
}

public class CellBlueprintDTO
{
    public BlueprintTileMapDTO TileMap { get; set; }
    public string[] Entities { get; set; } = [];
}
