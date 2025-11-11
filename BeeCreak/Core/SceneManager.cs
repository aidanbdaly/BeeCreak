using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class SceneManager(
        ContentManager contentManager,
        SceneCollection sceneCollection
    )
    {
        private readonly ContentManager contentManager = contentManager;

        private readonly SceneCollection scenes = sceneCollection;

        public SceneCollection Scenes => scenes;

        private Context context;

        private RenderTarget2D renderTarget;

        public Rectangle DestinationRectangle { get; private set; }

        public IScene Scene { get; private set; }

        public void RecomputeScaleUp()
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

            DestinationRectangle = new Rectangle(x, y, drawW, drawH);
        }

        public void UnloadScene(CancellationToken ct = default)
        {
            if (Scene is null || renderTarget is null)
            {
                return;
            }

            Scene.Dispose();
            renderTarget.Dispose();
            renderTarget = null;
        }

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            context = new Context(
                contentManager: contentManager,
                graphicsDevice: graphicsDevice,
                inputManager: new InputManager(this),
                sceneManager: this
            );
        }

        public void LoadScene(string sceneId)
        {
            Scene = scenes.Get(context, sceneId);

            Scene.LoadContent();

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
            if (Scene == null || renderTarget == null)
            {
                throw new InvalidOperationException("No scene loaded.");
            }

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

            if (DestinationRectangle.Width > 0 && DestinationRectangle.Height > 0)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
                spriteBatch.Draw(renderTarget, DestinationRectangle, Color.White);
                spriteBatch.End();
            }
        }
    }
}
