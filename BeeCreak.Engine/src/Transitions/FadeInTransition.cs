using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Transitions
{
    public sealed class FadeInTransition : Renderable, ITransition, Components.IUpdateable
    {
        private readonly float duration;

        private readonly GraphicsDevice graphicsDevice;

        public FadeInTransition(GraphicsDevice graphicsDevice, float duration)
        {
            this.graphicsDevice = graphicsDevice;
            this.duration = duration;
        }

        public override void Initialize()
        {
            Overlay = new Texture2D(graphicsDevice, 1, 1); Overlay.SetData([Color.White]);
        }

        private float Opacity { get; set; } = 1f;

        private Texture2D Overlay { get; set; }

        private TaskCompletionSource TransitionCompletionSource { get; set; }

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
                spriteBatch.Begin();
                spriteBatch.Draw(Overlay, graphicsDevice.PresentationParameters.Bounds, Color.Black * Opacity);
                spriteBatch.End();
            }
        }
    }
}