using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services
{
    public sealed class Canvas(SpriteBatch spriteBatch, Point size)
    {
        public RenderTarget2D renderTarget = new(
            spriteBatch.GraphicsDevice,
            size.X,
            size.Y,
            false,
            SurfaceFormat.Color,
            DepthFormat.None);

        public int Scale { get; private set; }

        public Point OriginalSize => size;

        public Point ScaledSize { get; private set; }

        public Point Offset { get; private set; }

        private Color Clear = Color.Wheat;

        private Rectangle Bounds = Rectangle.Empty;

        internal void Initialize()
        {
            var viewportSize = spriteBatch.GraphicsDevice.Viewport.Bounds.Size;

            var scale = viewportSize / size;

            Scale = Math.Min(scale.X, scale.Y);

            ScaledSize = new Point(size.X * Scale, size.Y * Scale);

            Offset = (viewportSize - ScaledSize) / new Point(2);

            Bounds = new Rectangle(Offset, ScaledSize);
        }

        internal void Dispose() => renderTarget.Dispose();

        internal void BeginDraw()
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.GraphicsDevice.Clear(Clear);
        }

        internal void EndDraw()
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, Bounds, Color.White);
            spriteBatch.End();
        }
    }
}