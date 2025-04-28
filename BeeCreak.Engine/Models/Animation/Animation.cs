using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    public Texture2D? Image { get; set; }

    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}