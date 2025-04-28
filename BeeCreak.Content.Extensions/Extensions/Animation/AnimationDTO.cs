using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.Data.Models;

internal class AnimationDTO
{
    public string ImageName { get; set; }
    
    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}