using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class FadeOutTransition : ITransition
    {
        private readonly float duration;

        private readonly Action onTransitionEnd;

        private readonly GraphicsDevice graphicsDevice;

        public FadeOutTransition(GraphicsDevice graphicsDevice, float duration, Action onTransitionEnd)
        {
            this.duration = duration;
            this.onTransitionEnd = onTransitionEnd;

            Overlay = new Texture2D(graphicsDevice, 1, 1);
            Overlay.SetData([Color.White]);
        }

        private float CurrentTime { get; set; } = 0f;

        private Texture2D Overlay { get; set; }

        private Rectangle Position { get; set; }

        public void Update(GameTime gameTime)
        {
            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentTime > duration)
            {
                onTransitionEnd.Invoke();
            }
        }

        public void Layout(GameWindow window)
        {
            Position = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var opacity = CurrentTime / duration;

            spriteBatch.Begin();
            spriteBatch.Draw(Overlay, Position, Color.Black * opacity);
            spriteBatch.End();
        }
    }
}