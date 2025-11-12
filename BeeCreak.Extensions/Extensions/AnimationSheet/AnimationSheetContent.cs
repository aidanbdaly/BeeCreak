using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Extensions.AnimationSheet;

public sealed class AnimationSheetContent
{
    public string Id { get; set; }

    public string Image { get; set; }

    public Dictionary<string, List<Rectangle>> Animations { get; } = new();
}
