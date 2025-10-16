using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Content.Pipeline.Extensions;

public class AnimationDTO
{
    public string ImageName { get; set; }

    public int TimePerFrame { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}