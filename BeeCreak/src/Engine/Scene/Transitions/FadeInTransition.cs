using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class FadeInTransition : ITransition
    {
        private readonly float duration;

        private readonly Action onTransitionEnd;

        public FadeInTransition(GraphicsDevice graphicsDevice, float duration, Action onTransitionEnd)
        {
            this.duration = duration;
            this.onTransitionEnd = onTransitionEnd;

            Overlay = new Texture2D(graphicsDevice, 1, 1);
            Overlay.SetData([Color.White]);
        }

        private float Opacity { get; set; } = 1f;

        private Texture2D Overlay { get; set; }

        private Rectangle ClientBounds { get; set; }

        public void Update(GameTime gameTime)
        {
            Opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds / duration;
            
            if (Opacity <= 0)
            {
                onTransitionEnd.Invoke();
            }
        }

        public void Layout(GameWindow window)
        {
            ClientBounds = window.ClientBounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Overlay, ClientBounds, Color.Black * Opacity);
            spriteBatch.End();
        }
    }
}