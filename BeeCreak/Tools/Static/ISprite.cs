namespace BeeCreak.Tools.Static
{
    using Microsoft.Xna.Framework.Graphics;

    public interface ISprite
    {
        GraphicsDevice GraphicsDevice { get; }

        SpriteBatch Batch { get; }

        Texture2D GetTexture(string textureName);

        SpriteFont GetFont(string fontName);

        System.Drawing.Rectangle GetBounds(string textureName);
    }
}