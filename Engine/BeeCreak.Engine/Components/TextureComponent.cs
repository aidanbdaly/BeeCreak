using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Graphics
{
    public sealed class TextureComponent(
        App app,
        Texture2D texture
    ) : DrawableGameComponent(app)
    {
        public State<Rectangle> Destination { get; init; } = new(Rectangle.Empty);

        public State<Rectangle> Source { get; init; } = new(Rectangle.Empty);

        public State<Color> Color { get; init; } = new(default);

        public State<float> Opacity { get; init; } = new(1f);

        public State<float> Rotation { get; init; } = new(0f);

        public State<Vector2> Origin { get; init; } = new(default);

        public State<SpriteEffects> Effects { get; init; } = new(default);

        public State<float> LayerDepth { get; init; } = new(0f);

        public Rectangle BoundingBox => Destination.Value;

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.Draw(
                texture,
                Destination.Value,
                Source.Value,
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Effects.Value,
                LayerDepth.Value);
        }
    }
}
