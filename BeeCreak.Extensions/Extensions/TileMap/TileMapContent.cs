using System.Collections.Generic;
using BeeCreak.Extensions.SpriteSheet;
using BeeCreak.Extensions.BoundingBoxSheet;

namespace BeeCreak.Extensions.TileMap;

public sealed class TileMapContent
{
    public string Id { get; set; }

    public SpriteSheetContent SpriteSheet { get; set; }

    public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

    public List<TileMapTileContent> Tiles { get; } = [];
}

public sealed class TileMapTileContent
{
    public int X { get; set; }

    public int Y { get; set; }

    public string Sprite { get; set; }
}
