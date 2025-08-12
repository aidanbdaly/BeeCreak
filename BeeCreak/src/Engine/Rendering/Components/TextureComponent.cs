using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class TextureComponent : IComponent, IDisposable
    {
        private readonly Texture2D texture;

        public TextureComponent(Texture2D texture)
        {
            this.texture = texture;
        }

        public bool IsEnabled { get; set; } = true;

        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Scale { get; set; } = 1f;

        public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;

        public Color Color { get; set; } = Color.White;

        public float Rotation { get; set; } = 0f;

        public Vector2 Origin { get; set; } = Vector2.Zero;

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public float LayerDepth { get; set; } = 0f;

        public virtual void Dispose()
        {
            texture?.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual Rectangle GetBounds() { return Rectangle.Empty; }

        public virtual Rectangle GetTextureBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                spriteBatch.Draw(texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
            }
        }
    }
}
