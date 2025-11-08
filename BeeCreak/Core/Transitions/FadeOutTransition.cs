using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Transitions
{
    public sealed class FadeOutTransition : Renderable, ITransition, IUpdateable
    {
        private readonly float duration;

        private readonly GraphicsDevice graphicsDevice;

        public FadeOutTransition(GraphicsDevice graphicsDevice, float duration)
        {
            this.graphicsDevice = graphicsDevice;
            this.duration = duration;
            IsEnabled = false;
        }

        private float CurrentTime { get; set; } = 0f;

        private Texture2D Overlay { get; set; }

        private TaskCompletionSource TransitionCompletionSource { get; set; } = new TaskCompletionSource();

        public Task PlayAsync(CancellationToken ct)
        {
            TransitionCompletionSource = new TaskCompletionSource();

            try
            {
                IsEnabled = true;
                if (ct.CanBeCanceled)
                {
                    ct.Register(() =>
                    {
                        IsEnabled = false;
                        TransitionCompletionSource.TrySetCanceled(ct);
                    });
                }
            }
            catch (Exception e)
            {
                IsEnabled = false;
                TransitionCompletionSource.TrySetException(e);
            }

            return TransitionCompletionSource.Task;
        }

        public override void Initialize()
        {
            Overlay = new Texture2D(graphicsDevice, 1, 1);
            Overlay.SetData([Color.White]);
        }

        public override Rectangle GetBounds()
        {
            return graphicsDevice.PresentationParameters.Bounds;
        }

        public override void Dispose()
        {
            Overlay.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (CurrentTime > duration)
                {
                    IsEnabled = false;
                    TransitionCompletionSource.TrySetResult();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                var opacity = CurrentTime / duration;

                spriteBatch.Draw(Overlay, graphicsDevice.PresentationParameters.Bounds, Color.Black * opacity);
            }
        }
    }
}
