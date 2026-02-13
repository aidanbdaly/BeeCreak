using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public sealed class TranstionComponent(App app) : DrawableGameComponent(app)
    {
        public Color Color { get; init; } = Color.Black;

        public float DurationInSeconds { get; init; } = 1f;

        private Rectangle Destination = Rectangle.Empty;

        private float Opacity = 1f;

        private float ElapsedTime = 0f;

        private Texture2D? Texture;

        protected override void LoadContent()
        {
            Texture = new Texture2D(app.GraphicsDevice, 1, 1);
            Texture.SetData([Color]);

            Destination = app.ScreenService.Bounds();
        }

        protected override void UnloadContent()
        {
            Texture?.Dispose();
            Texture = null;
        }

        public override void Update(GameTime gameTime)
        {
            ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Opacity = (DurationInSeconds - ElapsedTime) / DurationInSeconds;

            if (ElapsedTime >= DurationInSeconds)
            {
                Enabled = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp
            );

            app.SpriteBatch.Draw(
                Texture,
                Destination,
                null,
                Color * Opacity,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);

            app.SpriteBatch.End();
        }
    }
}