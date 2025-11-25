using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public record RenderableState(
        State<Vector2>? Position = null,
        State<Color>? Color = null,
        State<float>? Opacity = null,
        State<float>? Rotation = null,
        State<Vector2>? Origin = null,
        State<Vector2>? Scale = null,
        State<SpriteEffects>? Effects = null,
        State<float>? LayerDepth = null
    );

    public abstract class Renderable(
    State<Vector2>? position = null,
    State<Color>? color = null,
    State<float>? opacity = null,
    State<float>? rotation = null,
    State<Vector2>? origin = null,
    State<Vector2>? scale = null,
    State<SpriteEffects>? effects = null,
    State<float>? layerDepth = null
    ) : IRenderable
    {
        public Renderable(RenderableState state) : this(
            state.Position,
            state.Color,
            state.Opacity,
            state.Rotation,
            state.Origin,
            state.Scale,
            state.Effects,
            state.LayerDepth)
        { }

        public State<Vector2> Position { get; set; } = position ?? new(Vector2.Zero);

        public void SetPosition(Func<Vector2, Vector2> setStateDelegate)
        {
            Position.Value = setStateDelegate(Position.Value);
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position.Value = newPosition;
        }

        public State<Color> Color { get; set; } = color ?? new(default);

        public void SetColor(Func<Color, Color> setStateDelegate)
        {
            Color.Value = setStateDelegate(Color.Value);
        }

        public void SetColor(Color newColor)
        {
            Color.Value = newColor;
        }

        public State<float> Opacity { get; set; } = opacity ?? new(1f);

        public void SetOpacity(Func<float, float> setStateDelegate)
        {
            Opacity.Value = setStateDelegate(Opacity.Value);
        }

        public void SetOpacity(float newOpacity)
        {
            Opacity.Value = newOpacity;
        }

        public State<float> Rotation { get; set; } = rotation ?? new(0f);

        public void SetRotation(Func<float, float> setStateDelegate)
        {
            Rotation.Value = setStateDelegate(Rotation.Value);
        }

        public void SetRotation(float newRotation)
        {
            Rotation.Value = newRotation;
        }

        public State<Vector2> Origin { get; set; } = origin ?? new(default);

        public void SetOrigin(Func<Vector2, Vector2> setStateDelegate)
        {
            Origin.Value = setStateDelegate(Origin.Value);
        }

        public void SetOrigin(Vector2 newOrigin)
        {
            Origin.Value = newOrigin;
        }

        public State<Vector2> Scale { get; set; } = scale ?? new(Vector2.One);

        public void SetScale(Func<Vector2, Vector2> setStateDelegate)
        {
            Scale.Value = setStateDelegate(Scale.Value);
        }

        public void SetScale(Vector2 newScale)
        {
            Scale.Value = newScale;
        }

        public State<SpriteEffects> Effects { get; set; } = effects ?? new(default);

        public void SetEffects(Func<SpriteEffects, SpriteEffects> setStateDelegate)
        {
            Effects.Value = setStateDelegate(Effects.Value);
        }

        public void SetEffects(SpriteEffects newEffects)
        {
            Effects.Value = newEffects;
        }

        public State<float> LayerDepth { get; set; } = layerDepth ?? new(0f);

        public void SetLayerDepth(Func<float, float> setStateDelegate)
        {
            LayerDepth.Value = setStateDelegate(LayerDepth.Value);
        }

        public void SetLayerDepth(float newLayerDepth)
        {
            LayerDepth.Value = newLayerDepth;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract Rectangle Bounds { get; }
    }
}
