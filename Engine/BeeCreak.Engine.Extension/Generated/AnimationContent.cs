using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class AnimationContent
{
public string Id { get; set; }

public SpriteSheetContent SpriteSheet { get; set; }

public List<string> Data { get; } = new();

}
