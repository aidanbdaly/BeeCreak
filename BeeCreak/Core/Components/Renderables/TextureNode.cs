using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public class TextureNode(Texture2D texture) : Renderable
    {
        private readonly Texture2D texture = texture;

        public Rectangle? SourceRectangle { get; set; } = null;

        public override Rectangle GetBounds()
        {
            var width = (SourceRectangle?.Width ?? 0) * Scale;
            var height = (SourceRectangle?.Height ?? 0) * Scale;

            return new Rectangle(
                (int)(Position.X - Origin.X),
                (int)(Position.Y - Origin.Y),
                (int)width,
                (int)height
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
