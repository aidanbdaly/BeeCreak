using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BeeCreak.Engine.Transitions;

namespace BeeCreak.Engine.Transitions
{
    public class FadeInTransition : ITransition
    {
        private readonly float duration;

        private readonly GraphicsDevice graphicsDevice;

        public FadeInTransition(GraphicsDevice graphicsDevice, float duration)
        {
            this.graphicsDevice = graphicsDevice;
            this.duration = duration;

            Overlay = new Texture2D(graphicsDevice, 1, 1);
            Overlay.SetData([Color.White]);
        }

        private bool IsPlaying { get; set; }

        private float Opacity { get; set; } = 1f;

        private Texture2D Overlay { get; set; }

        private Rectangle DestinationRectangle { get; set; }

        private TaskCompletionSource TransitionCompletionSource { get; set; }

        public Task PlayAsync(CancellationToken ct)
        {
            TransitionCompletionSource = new TaskCompletionSource();

            try
            {
                IsPlaying = true;
                if (ct.CanBeCanceled)
                {
                    ct.Register(() =>
                    {
                        IsPlaying = false;
                        TransitionCompletionSource.TrySetCanceled(ct);
                    });
                }
            }
            catch (Exception e)
            {
                IsPlaying = false;
                TransitionCompletionSource.TrySetException(e);
            }

            return TransitionCompletionSource.Task;
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                Opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds / duration;

                if (Opacity <= 0)
                {
                    IsPlaying = false;
                    TransitionCompletionSource.TrySetResult();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsPlaying)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(Overlay, graphicsDevice.PresentationParameters.Bounds, Color.Black * Opacity);
                spriteBatch.End();
            }
        }
    }
}