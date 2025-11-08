using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Transitions
{
    public sealed class FadeInTransition(GraphicsDevice graphicsDevice, float duration) : Renderable, ITransition, IUpdateable
    {
        private readonly float duration = duration;

        private readonly GraphicsDevice graphicsDevice = graphicsDevice;

        public override void Initialize()
        {
            Overlay = new Texture2D(graphicsDevice, 1, 1); Overlay.SetData([Color.White]);
        }

        private float Opacity { get; set; } = 1f;

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

        public void Update(GameTime gameTime)
        {
            if (IsEnabled)
            {
                Opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds / duration;

                if (Opacity <= 0)
                {
                    IsEnabled = false;
                    TransitionCompletionSource.TrySetResult();
                }
            }
        }

        public override Rectangle GetBounds()
        {
            return graphicsDevice.PresentationParameters.Bounds;
        }

        public override void Dispose()
        {
            Overlay.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                spriteBatch.Draw(Overlay, new Rectangle(0, 0, 640, 360), Color.Black * Opacity);
            }
        }
    }
}
