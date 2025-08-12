using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.src.Models;

public class SpriteSheet : IDisposable
{
    public Texture2D Image { get; set; }

    public Dictionary<string, Rectangle> Frames { get; set; }

    public int Resolution { get; set; }

    public void Dispose()
    {
        Image?.Dispose();
        Image = null;
        Frames.Clear();
        Frames = null;
    }
}