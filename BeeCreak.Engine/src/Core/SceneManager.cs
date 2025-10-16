using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Transitions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Core
{
    public record SceneServices(
        AssetManager AssetManager,
        GraphicsDevice GraphicsDevice,
        Func<Point> GetMousePosition,
        Func<string, CancellationToken, Task> ChangeScene
    );

    public class SceneManager
    {
        private readonly TransitionFactory transitionFactory;

        private readonly SceneFactory sceneFactory;

        public SceneManager(
            SceneFactory sceneFactory,
            TransitionFactory transitionFactory,
            GraphicsDevice graphicsDevice,
            AssetManager assetManager,
            )
        {
            this.sceneFactory = sceneFactory;
            this.transitionFactory = transitionFactory;

            Services = new SceneServices(
                AssetManager: assetManager,
                GraphicsDevice: graphicsDevice,
                GetMousePosition: GetMousePosition,
                ChangeScene: ChangeSceneAsync
            );
        }

        private RenderTarget2D renderTarget;

        private Rectangle destinationRectangle;

        public IScene Scene { get; set; }

        public ITransition Transition { get; private set; }

        public SceneServices Services { get; private set; }

        private void CreateRenderTarget()
        {
            renderTarget?.Dispose();

            if (Scene != null && Scene.Width > 0 && Scene.Height > 0)
            {
                renderTarget = new RenderTarget2D(
                    Services.GraphicsDevice,
                    Scene.Width,
                    Scene.Height,
                    false,
                    SurfaceFormat.Color,
                    DepthFormat.None,
                    0,
                    RenderTargetUsage.DiscardContents);

                RecomputeScaleUp();
            }
        }

        private void RecomputeScaleUp()
        {
            if (renderTarget == null)
                return;

            int backW = Services.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int backH = Services.GraphicsDevice.PresentationParameters.BackBufferHeight;

            int nativeW = renderTarget.Width;
            int nativeH = renderTarget.Height;

            float widthRatio = backW / (float)nativeW;
            float heightRatio = backH / (float)nativeH;

            var scale = Math.Max(1, Math.Min(widthRatio, heightRatio));

            int drawW = (int)(nativeW * scale);
            int drawH = (int)(nativeH * scale);

            int x = (backW - drawW) / 2;  // pillarbox if any
            int y = (backH - drawH) / 2;  // letterbox if any

            destinationRectangle = new Rectangle(x, y, drawW, drawH);
        }

        public Point GetMousePosition()
        {
            if (renderTarget == null)
                return Point.Zero;

            var mousePosition = Mouse.GetState().Position;

            float scaleX = destinationRectangle.Width / (float)renderTarget.Width;
            float scaleY = destinationRectangle.Height / (float)renderTarget.Height;
            int offX = destinationRectangle.X;
            int offY = destinationRectangle.Y;

            return new Point(

                (int)((mousePosition.X - offX) / scaleX),
                (int)((mousePosition.Y - offY) / scaleY));
        }

        public async Task ExitSceneAsync(CancellationToken ct = default)
        {
            if (Scene is { })
            {
                try
                {
                    Transition = transitionFactory.GetTransition(Scene.ExitTransition, 1);
                    await Transition.PlayAsync(ct);
                }
                finally
                {
                    Scene.Dispose();
                    renderTarget?.Dispose();
                    renderTarget = null;
                }
            }
        }

        private async Task EnterSceneAsync(CancellationToken ct = default)
        {
            if (Scene is { })
            {
                Transition = transitionFactory.GetTransition(Scene.EntranceTransition, 1);
                await Transition.PlayAsync(ct);
            }
        }

        public async Task ChangeSceneAsync(string sceneId, CancellationToken ct = default)
        {
            await ExitSceneAsync(ct);

            Scene = sceneFactory.GetScene(Context, Services, sceneId);

            if (Scene != null)
            {
                CreateRenderTarget();
            }

            await EnterSceneAsync(ct);
        }

        public void OnWindowResize() => RecomputeScaleUp();

        public void Update(GameTime gameTime)
        {
            Scene?.Update(gameTime);
            Transition.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Scene != null && renderTarget != null)
            {
                Services.GraphicsDevice.SetRenderTarget(renderTarget);
                Services.GraphicsDevice.Clear(Scene.Clear);

                spriteBatch.Begin(
                    sortMode: SpriteSortMode.Deferred,
                    blendState: BlendState.AlphaBlend,
                    samplerState: SamplerState.PointClamp,
                    depthStencilState: DepthStencilState.None,
                    rasterizerState: RasterizerState.CullNone
                );

                Scene.Draw(spriteBatch);

                spriteBatch.End();

                Services.GraphicsDevice.SetRenderTarget(null);

                if (destinationRectangle.Width > 0 && destinationRectangle.Height > 0)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
                    spriteBatch.Draw(renderTarget, destinationRectangle, Color.White);
                    spriteBatch.End();
                }
            }

            Transition?.Draw(spriteBatch);
        }
    }
}