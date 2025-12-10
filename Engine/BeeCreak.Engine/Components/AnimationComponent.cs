using BeeCreak.Engine.Data.Models;
using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Graphics
{
    public class AnimationComponent(
        App app,
        State<Animation> animation,
        State<float> intervalInSeconds) : DrawableGameComponent(app)
    {
        public State<Vector2> Position { get; set; } = new(Vector2.Zero);

        public State<Color> Color { get; set; } = new(Microsoft.Xna.Framework.Color.White);

        public State<float> Opacity { get; set; } = new(1f);

        public State<float> Rotation { get; set; } = new(0f);

        public State<Vector2> Origin { get; set; } = new(default);

        public State<Vector2> Scale { get; set; } = new(Vector2.One);

        public State<SpriteEffects> Effect { get; set; } = new(default);

        public State<float> LayerDepth { get; set; } = new(0f);

        private float Elapsed = 0f;

        private int FrameIndex = 0;

        public override void Update(GameTime gameTime)
        {
            Elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Elapsed >= intervalInSeconds.Value)
            {
                Elapsed -= intervalInSeconds.Value;
                FrameIndex = (FrameIndex + 1) % animation.Value.Data.Count;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.Draw(
                animation.Value.Texture,
                Position.Value,
                animation.Value.Data[FrameIndex],
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Scale.Value,
                Effect.Value,
                LayerDepth.Value
            );
        }
    }
}