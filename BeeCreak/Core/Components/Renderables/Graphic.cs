using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components.Renderables
{
    public class Graphic(
        GraphicsDevice graphicsDevice,
        Point size,
        State<Rectangle>? destination = default,
        State<Rectangle>? source = default,
        State<Color>? color = default,
        State<float>? opacity = default,
        State<float>? rotation = default,
        State<Vector2>? origin = default,
        State<SpriteEffects>? effects = default,
        State<float>? layerDepth = default) : Renderable(default, color, opacity, rotation, origin, default, effects, layerDepth)
    {
        private readonly Texture2D texture = new(graphicsDevice, size.X, size.Y);

        public State<Rectangle> Destination { get; set; } = destination ?? new(Rectangle.Empty);

        public State<Rectangle> Source { get; set; } = source ?? new(new(Point.Zero, size));

        public override Rectangle Bounds => Destination.Value;

        public void SetData(Color[] data) => texture.SetData(data);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
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