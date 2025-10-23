using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class Texture(Texture2D texture) : Renderable
    {
        private readonly Texture2D texture = texture;

        public Rectangle? SourceRectangle { get; set; } = null;

        public override Rectangle GetBounds()
        {
            return new Rectangle(
                (int)(Position.X - Origin.X),
                (int)(Position.Y - Origin.Y),
                (int)(texture.Width * Scale),
                (int)(texture.Height * Scale)
            );
        }

        public override void Dispose()
        {
            texture?.Dispose();
            GC.SuppressFinalize(this);
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
