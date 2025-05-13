using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace BeeCreak.Content.Extensions;

public class AnimationDTO
{
    public string ImageName { get; set; }
    
    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}