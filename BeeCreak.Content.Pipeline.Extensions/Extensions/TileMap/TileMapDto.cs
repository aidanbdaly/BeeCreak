using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.TileMap;

public sealed class TileMapDto
{
    public string Id { get; set; }

    public string SpriteSheet { get; set; }

    public Dictionary<string, string> Tiles { get; set; } = [];
}
