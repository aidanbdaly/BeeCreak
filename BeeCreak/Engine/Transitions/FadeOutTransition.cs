using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Transitions
{
    public class FadeOutTransition : ITransition
    {
        private readonly float duration;

        public FadeOutTransition(GraphicsDevice graphicsDevice, float duration)
        {
            this.duration = duration;

            Overlay = new Texture2D(graphicsDevice, 1, 1);
            Overlay.SetData([Color.White]);
            DestinationRectangle = new Rectangle(0, 0, graphicsDevice.PresentationParameters.Bounds.Width, graphicsDevice.PresentationParameters.Bounds.Height);
        }

        private bool IsPlaying { get; set; }

        private float CurrentTime { get; set; } = 0f;

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
                CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (CurrentTime > duration)
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
                var opacity = CurrentTime / duration;

                spriteBatch.Begin();
                spriteBatch.Draw(Overlay, DestinationRectangle, Color.Black * opacity);
                spriteBatch.End();
            }
        }
    }
}