using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class Scene(GraphicsDevice graphicsDevice, int width, int height) : IScene
    {
        private readonly SpriteBatch spriteBatch = new(graphicsDevice);

        private readonly RenderTarget2D renderTarget = new(
            graphicsDevice,
            width,
            height,
            false,
            SurfaceFormat.Color,
            DepthFormat.None
        );

        public Rectangle DestinationRectangle { get; private set; } = GetDestinationRectangle(graphicsDevice, width, height);

        public void OnWindowResize()
        {
            DestinationRectangle = GetDestinationRectangle(graphicsDevice, width, height);
        }

        private static Rectangle GetDestinationRectangle(GraphicsDevice graphicsDevice, int sceneWidth, int sceneHeight)
        {
            int backW = graphicsDevice.PresentationParameters.BackBufferWidth;
            int backH = graphicsDevice.PresentationParameters.BackBufferHeight;

            int nativeW = sceneWidth;
            int nativeH = sceneHeight;

            float widthRatio = backW / (float)nativeW;
            float heightRatio = backH / (float)nativeH;

            var scale = Math.Max(1, Math.Min(widthRatio, heightRatio));

            int drawW = (int)(nativeW * scale);
            int drawH = (int)(nativeH * scale);

            int x = (backW - drawW) / 2;  // pillarbox if any
            int y = (backH - drawH) / 2;  // letterbox if any

            return new Rectangle(x, y, drawW, drawH);
        }

        private readonly List<IComponent> components = [];

        public required Point Size { get; init; } = new(width, height);

        public Color Clear { get; set; } = Color.Wheat;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var component in components)
            {
                if (component is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public Action AddComponent(IComponent component)
        {
            components.Add(component);
            return () => components.Remove(component);
        }

        public virtual void LoadContent() { }

        public void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                if (component is Components.IUpdateable updateable)
                {
                    updateable.Update(gameTime);
                }
            }
        }

        public void Draw()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(Clear);

            spriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp,
                depthStencilState: DepthStencilState.None,
                rasterizerState: RasterizerState.CullNone
            );

            foreach (var component in components)
            {
                if (component is IRenderable renderable)
                {
                    renderable.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, DestinationRectangle, Color.White);
            spriteBatch.End();
        }
    }
}