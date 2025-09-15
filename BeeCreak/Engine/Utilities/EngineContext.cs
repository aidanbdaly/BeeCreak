using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public static class EngineContext
    {
        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public static GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;

        public static SpriteBatch SpriteBatch { get; private set; }

        internal static void SetGraphicsDeviceManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
        }

        internal static void Initialize()
        {
            if (GraphicsDeviceManager == null)
            {
                throw new InvalidOperationException("GraphicsDeviceManager has not been initialized.");
            }

            GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            GraphicsDeviceManager.ApplyChanges();

            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public static RenderTarget2D CreateRenderTarget(int width, int height)
        {
            return new RenderTarget2D(
                GraphicsDevice,
                width,
                height,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.DiscardContents);
        }
    }
}