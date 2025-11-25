using BeeCreak.Core.Models;
using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components 
{
    public sealed class Sprite(
        SpriteSheet spriteSheet,
        string spriteName,
        State<Vector2>? position = default,
        State<Color>? color = default,
        State<float>? opacity = default,
        State<float>? rotation = default,
        State<Vector2>? origin = default,
        State<Vector2>? scale = default,
        State<SpriteEffects>? effects = default,
        State<float>? layerDepth = default) :
        Renderable(position, color, opacity, rotation, origin, scale, effects, layerDepth)
    {
        public Sprite(
            SpriteSheet spriteSheet,
            string spriteName,
            RenderableState state) : this(
                spriteSheet,
                spriteName,
                state.Position,
                state.Color,
                state.Opacity,
                state.Rotation,
                state.Origin,
                state.Scale,
                state.Effects,
                state.LayerDepth)
        { }

        private Rectangle sourceRectangle = spriteSheet.Frames[spriteName];

        public void SetSprite(string spriteName)
        {
            sourceRectangle = spriteSheet.Frames[spriteName];
        }

        public override Rectangle Bounds => new(Position.Value.ToPoint(), (sourceRectangle.Size.ToVector2() * Scale.Value).ToPoint());

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                spriteSheet.Texture,
                Position.Value,
                sourceRectangle,
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Scale.Value,
                Effects.Value,
                LayerDepth.Value);
        }
    }
}
