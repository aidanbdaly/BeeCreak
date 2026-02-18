using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class TileMapContent
{
public string Id { get; set; }

public SpriteSheetContent SpriteSheet { get; set; }

public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

public List<List<string>> Data { get; } = new();

}
