using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Tools.Static;

public class Sprite : ISprite
{
    public Sprite()
    {
    }

    public GraphicsDevice GraphicsDevice { get; set; }

    public SpriteBatch Batch { get; set; }

    private SpriteFont Font { get; set; }

    public void Load(SpriteFont font, GraphicsDevice graphicsDevice)
    {
        Batch = new SpriteBatch(graphicsDevice);
        
        Font = font;
        GraphicsDevice = graphicsDevice;
    }

    public void DrawString(string text, Vector2 position, Color color, float rotation, bool centerOrigin, float scale, SpriteEffects effects, float layerDepth)
    {
        var origin = centerOrigin ? Font.MeasureString(text) / 2 : Vector2.Zero;

        Batch.DrawString(Font, text, position, color, rotation, origin, scale, effects, layerDepth);
    }

    public Vector2 MeasureString(string text)
    {
        return Font.MeasureString(text);
    }
}
