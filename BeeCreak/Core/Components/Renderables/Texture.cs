using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    // Need two variants, non disposable for assets from content manager (content manager disposes on unload)
    // And a disposable one for user created textures (transition overlay)
    // Or maybe we just don't, and we just dispose the user created textures somewhere else

    // Load assets in a non-disposable form <----------
    public sealed class TextureComponent : Renderable
    {
        private readonly Texture2D texture;

        public State<Rectangle>? DestinationRectangle { get; set; }

        public State<Rectangle> SourceRectangle { get; set; }

        public TextureComponent(
        Texture2D texture,
        Rectangle destinationRectangle,
        Rectangle sourceRectangle = default,
        Color color = default,
        float rotation = 0f,
        Vector2 origin = default,
        SpriteEffects effects = SpriteEffects.None,
        float layerDepth = 0f) : base(default, color, rotation, origin, default, effects, layerDepth)
        {
            this.texture = texture;
            DestinationRectangle = new(destinationRectangle);
            SourceRectangle = new(sourceRectangle);
        }

        public TextureComponent(
            Texture2D texture,
            Vector2 position = default,
            Rectangle sourceRectangle = default,
            Color color = default,
            float rotation = 0f,
            Vector2 origin = default,
            Vector2 scale = default,
            SpriteEffects effects = SpriteEffects.None,
            float layerDepth = 0f) : base(position, color, rotation, origin, scale, effects, layerDepth)
        {
            this.texture = texture;
            SourceRectangle = new(sourceRectangle);
        }

        public override Rectangle GetBounds()
        {
            var destinationRectangle = DestinationRectangle?.Value;

            if (destinationRectangle.HasValue)
            {
                return destinationRectangle.Value;
            }
            else
            {
                var size = SourceRectangle.Value == Rectangle.Empty
                    ? texture.Bounds.Size.ToVector2() * Scale
                    : SourceRectangle.Value.Size.ToVector2() * Scale;

                return new Rectangle(
                    Position.Value.ToPoint(),
                    size.ToPoint());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var destinationRectangle = DestinationRectangle?.Value;

            if (destinationRectangle.HasValue)
            {
                spriteBatch.Draw(texture, destinationRectangle.Value, SourceRectangle.Value, Color, Rotation, Origin, Effects, LayerDepth);
            }
            else
            {
                spriteBatch.Draw(texture, Position.Value, SourceRectangle.Value, Color, Rotation, Origin, Scale, Effects, LayerDepth);
            }
        }
    }
}
