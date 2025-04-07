using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Data.Models;

public class Animation
{
    public string SpriteSheetName { get; set; }

    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}