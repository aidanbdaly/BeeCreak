using System.Collections.Generic;
using BeeCreak.Extensions.SpriteSheet;

namespace BeeCreak.Extensions.Animation;

public sealed class AnimationContent
{
    public string Id { get; set; }

    public SpriteSheetContent SpriteSheet { get; set; }

    public List<string> Data { get; } = [];
}
