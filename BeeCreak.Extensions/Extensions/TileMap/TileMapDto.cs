using System.Collections.Generic;

namespace BeeCreak.Extensions.TileMap;

public sealed class TileMapDto
{
    public string Id { get; set; }

    public string SpriteSheet { get; set; }

    public string BoundingBoxSheet { get; set; }

    public Dictionary<string, string> Tiles { get; set; } = [];
}
