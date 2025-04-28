using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

public class AnimationContent
{
    public Texture2DContent Image { get; set; }

    public List<Rectangle> Frames { get; set; } 

    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }
}