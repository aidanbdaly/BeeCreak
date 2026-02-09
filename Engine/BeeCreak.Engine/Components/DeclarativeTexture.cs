using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Graphics
{
    public sealed class DeclarativeTexture(
        App app,
        Texture2D texture,
        State<Rectangle>? destinationRectangle = default,
        State<Rectangle>? sourceRectangle = default,
        State<Color>? color = default,
        State<float>? opacity = default,
        State<float>? rotation = default,
        State<Vector2>? origin = default,
        State<SpriteEffects>? effects = default,
        State<float>? layerDepth = default) : DrawableGameComponent(app)
    {
        public State<Rectangle> DestinationRectangle { get; set; } = destinationRectangle ?? new(Rectangle.Empty);

        public State<Rectangle> SourceRectangle { get; set; } = sourceRectangle ?? new(Rectangle.Empty);

        public State<Color> Color { get; set; } = color ?? new(default);

        public State<float> Opacity { get; set; } = opacity ?? new(1f);

        public State<float> Rotation { get; set; } = rotation ?? new(0f);

        public State<Vector2> Origin { get; set; } = origin ?? new(default);

        public State<SpriteEffects> Effects { get; set; } = effects ?? new(default);

        public State<float> LayerDepth { get; set; } = layerDepth ?? new(0f);

        public Rectangle BoundingBox => DestinationRectangle.Value;

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.Draw(
                texture,
                DestinationRectangle.Value,
                SourceRectangle.Value,
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Effects.Value,
                LayerDepth.Value);
        }
    }
}
