namespace BeeCreak.Tools.Static;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface ISprite
{
    GraphicsDevice GraphicsDevice { get; }

    SpriteBatch Batch { get; }
    
    Vector2 MeasureString(string text);

    void Load(SpriteFont font, GraphicsDevice graphicsDevice);

    void DrawString(string text, Vector2 position, Color color, float rotation = 0, bool centerOrigin = false, float scale = 1, SpriteEffects effects = default, float layerDepth = 0);

}
