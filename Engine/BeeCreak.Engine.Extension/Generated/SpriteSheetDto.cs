using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class SpriteSheetDto
{
public string Id { get; set; }

public string Texture { get; set; }

public Dictionary<string, object> Data { get; set; } = new();

}
