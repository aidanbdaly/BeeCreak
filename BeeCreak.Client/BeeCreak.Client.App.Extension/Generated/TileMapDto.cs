using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class TileMapDto
{
public string Id { get; set; }

public string SpriteSheet { get; set; }

public string BoundingBoxSheet { get; set; }

public List<List<string>> Data { get; set; } = new();

}
