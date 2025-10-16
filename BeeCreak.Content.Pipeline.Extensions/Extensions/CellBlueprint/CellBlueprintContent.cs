namespace BeeCreak.Content.Pipeline.Extensions;

public class BlueprintTileMapContent
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string[] Data { get; set; }
}

public class CellBlueprintContent
{
    public BlueprintTileMapContent TileMap { get; set; }
    public string[] Entities { get; set; } = [];
}
