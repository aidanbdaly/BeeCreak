using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.Data.Models;

public class SpriteSheet
{
    public Texture2D? Image { get; set; }

    public Dictionary<string, Rectangle> Frames { get; set; } = new();

    public Texture2D GetSprite(string name)
    {
        if (Frames.TryGetValue(name, out var frame))
        {
            var texture = new Texture2D(Image?.GraphicsDevice, frame.Width, frame.Height);

            var data = new Color[frame.Width * frame.Height];

            Image?.GetData(0, new Rectangle(frame.X, frame.Y, frame.Width, frame.Height), data, 0, data.Length);

            texture.SetData(data);

            return texture;
        }

        throw new KeyNotFoundException($"Sprite '{name}' not found in spritesheet.");
    }
}