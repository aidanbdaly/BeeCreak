using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Core
{
    public class SceneManager(Context context)
    {
        private readonly SceneCollection scenes = new();

        public SceneCollection Scenes => scenes;

        private readonly Context context = context;

        private RenderTarget2D renderTarget;

        private Rectangle destinationRectangle;

        public IScene Scene { get; set; }

        public string StartScene { get; set; }

        public void OnWindowResized() => RecomputeScaleUp();

        private void RecomputeScaleUp()
        {
            if (renderTarget == null)
            {
                throw new InvalidOperationException("Render target is not created.");
            }

            int backW = context.graphicsDevice.PresentationParameters.BackBufferWidth;
            int backH = context.graphicsDevice.PresentationParameters.BackBufferHeight;

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

        public void UnloadScene(CancellationToken ct = default)
        {
            if (Scene is { })
            {
                Scene.Dispose();
                renderTarget?.Dispose();
                renderTarget = null;
            }
        }

        public void Startup()
        {
            if (string.IsNullOrEmpty(StartScene))
            {
                throw new InvalidOperationException("StartScene is not set.");
            }

            LoadScene(StartScene);
        }

        public void LoadScene(string sceneId)
        {
            Scene = scenes.Get(context, sceneId);

            if (Scene == null)
            {
                throw new InvalidOperationException($"Scene '{sceneId}' not found.");
            }

            if (!Scene.Validate())
            {
                throw new InvalidOperationException($"Scene '{sceneId}' has invalid dimensions.");
            }

            renderTarget = new RenderTarget2D(
                context.graphicsDevice,
                Scene.Width,
                Scene.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.DiscardContents);

            RecomputeScaleUp();
        }

        public void SwitchScene(string sceneId)
        {
            UnloadScene();
            LoadScene(sceneId);
        }

        public void Update(GameTime gameTime)
        {
            if (Scene == null)
            {
                throw new InvalidOperationException("No scene loaded.");
            }

            Scene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Scene != null && renderTarget != null)
            {
                context.graphicsDevice.SetRenderTarget(renderTarget);
                context.graphicsDevice.Clear(Scene.Clear);

                spriteBatch.Begin(
                    sortMode: SpriteSortMode.Deferred,
                    blendState: BlendState.AlphaBlend,
                    samplerState: SamplerState.PointClamp,
                    depthStencilState: DepthStencilState.None,
                    rasterizerState: RasterizerState.CullNone
                );

                Scene.Draw(spriteBatch);

                spriteBatch.End();

                context.graphicsDevice.SetRenderTarget(null);

                if (destinationRectangle.Width > 0 && destinationRectangle.Height > 0)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
                    spriteBatch.Draw(renderTarget, destinationRectangle, Color.White);
                    spriteBatch.End();
                }
            }
        }
    }
}
