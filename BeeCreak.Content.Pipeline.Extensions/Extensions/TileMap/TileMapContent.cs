using System.Collections.Generic;
using BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

namespace BeeCreak.Content.Pipeline.Extensions.TileMap;

public sealed class TileMapContent
{
    public string Id { get; set; }

    public SpriteSheetContent SpriteSheet { get; set; }

    public List<TileMapTileContent> Tiles { get; } = [];
}

public sealed class TileMapTileContent
{
    public int X { get; set; }

    public int Y { get; set; }

    public string Sprite { get; set; }
}
