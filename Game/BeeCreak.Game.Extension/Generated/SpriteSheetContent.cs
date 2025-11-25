using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class SpriteSheetContent
{
public string Id { get; set; }

public string Image { get; set; }

public Dictionary<string, object> Sprites { get; } = new();

}
