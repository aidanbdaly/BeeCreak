using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class TextureComponent : Component
    {
        private readonly Texture2D texture;

        public TextureComponent(Texture2D texture)
        {
            this.texture = texture;
        }

        public Rectangle? SourceRectangle { get; set; } = null;

        public override Rectangle GetBounds()
        {
            return new Rectangle(
                (int)WorldTransform.Position.X - (int)Origin.X,
                (int)WorldTransform.Position.Y - (int)Origin.Y,
                (int)(texture.Width * WorldTransform.Scale),
                (int)(texture.Height * WorldTransform.Scale)
                );
        }
        public override void Dispose()
        {
            texture?.Dispose();
            GC.SuppressFinalize(this);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                spriteBatch.Draw(texture, WorldTransform.Position, SourceRectangle, Color, WorldTransform.Rotation, Origin, WorldTransform.Scale, Effects, LayerDepth);
            }
        }
    }
}
