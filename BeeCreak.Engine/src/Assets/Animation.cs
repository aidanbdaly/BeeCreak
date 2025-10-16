using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Assets;

public class Animation
{
    public Texture2D Image { get; set; }

    public int TimePerFrame { get; set; }

    public List<Rectangle> Frames { get; set; } = new();
}