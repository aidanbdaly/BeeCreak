using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Utilities
{
    public static class VirtualDisplayManager
    {
        public static RenderTarget2D VirtualWindow { get; private set; }

        public static Rectangle DestinationRectangle { get; private set; }

        public static GraphicsDevice graphicsDevice => EngineContext.GraphicsDeviceManager.GraphicsDevice;

        public static void ResizeVirtualWindow(int width, int height)
        {
            VirtualWindow = EngineContext.CreateRenderTarget(width, height);
        }

        public static void RecomputeScaleUp()
        {
            int backW = graphicsDevice.PresentationParameters.BackBufferWidth;
            int backH = graphicsDevice.PresentationParameters.BackBufferHeight;

            int nativeW = VirtualWindow.Width;
            int nativeH = VirtualWindow.Height;

            float widthRatio = backW / (float)nativeW;
            float heightRatio = backH / (float)nativeH;

            var scale = Math.Max(1, Math.Min(widthRatio, heightRatio));

            int drawW = (int)(nativeW * scale);
            int drawH = (int)(nativeH * scale);

            int x = (backW - drawW) / 2;  // pillarbox if any
            int y = (backH - drawH) / 2;  // letterbox if any

            DestinationRectangle = new Rectangle(x, y, drawW, drawH);
        }

        public static Point GetVirtualMousePosition()
        {
            var mousePosition = Mouse.GetState().Position;

            float scaleX = DestinationRectangle.Width / (float)VirtualWindow.Width;
            float scaleY = DestinationRectangle.Height / (float)VirtualWindow.Height;
            int offX = DestinationRectangle.X;
            int offY = DestinationRectangle.Y;

            return new Point(
                (int)((mousePosition.X - offX) / scaleX),
                (int)((mousePosition.Y - offY) / scaleY));
        }

        public static void BeginVirtual(SpriteBatch spriteBatch)
        {
            if (VirtualWindow == null) return;

            graphicsDevice.SetRenderTarget(VirtualWindow);
        }

        public static void EndVirtual(SpriteBatch spriteBatch)
        {
            if (VirtualWindow == null) return;

            graphicsDevice.SetRenderTarget(null);

            if (VirtualWindow == null || DestinationRectangle.Width <= 0 || DestinationRectangle.Height <= 0)
                return;

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            spriteBatch.Draw(VirtualWindow, DestinationRectangle, Color.White);
            spriteBatch.End();
        }
    }
}