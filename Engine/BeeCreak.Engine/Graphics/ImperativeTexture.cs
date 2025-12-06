using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Graphics
{
    public class ImperativeTexture(
        App app,
        Texture2D texture,
        State<Vector2>? position = default,
        State<Rectangle>? sourceRectangle = default,
        State<Color>? color = default,
        State<float>? opacity = default,
        State<float>? rotation = default,
        State<Vector2>? origin = default,
        State<Vector2>? scale = default,
        State<SpriteEffects>? effects = default,
        State<float>? layerDepth = default) : Sprite(app)
    {
        public State<Vector2> Position { get; set; } = position ?? new(Vector2.Zero);

        public State<Rectangle> SourceRectangle { get; set; } = sourceRectangle ?? new(Rectangle.Empty);

        public State<Color> Color { get; set; } = color ?? new(default);

        public State<float> Opacity { get; set; } = opacity ?? new(1f);

        public State<float> Rotation { get; set; } = rotation ?? new(0f);

        public State<Vector2> Origin { get; set; } = origin ?? new(default);

        public State<Vector2> Scale { get; set; } = scale ?? new(Vector2.One);

        public State<SpriteEffects> Effects { get; set; } = effects ?? new(default);

        public State<float> LayerDepth { get; set; } = layerDepth ?? new(0f);

        public override Rectangle BoundingBox
            => new(Position.Value.ToPoint(), (SourceRectangle.Value.Size.ToVector2() * Scale.Value).ToPoint());

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.Draw(
                texture,
                Position.Value,
                SourceRectangle.Value,
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Scale.Value,
                Effects.Value,
                LayerDepth.Value);
        }
    }
}
