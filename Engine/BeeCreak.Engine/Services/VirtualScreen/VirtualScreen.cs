using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services
{
    public sealed class VirtualScreen(SpriteBatch spriteBatch, Point resolution)
    {
        private readonly RenderTarget2D renderTarget = new(
            spriteBatch.GraphicsDevice,
            resolution.X,
            resolution.Y,
            false,
            SurfaceFormat.Color,
            DepthFormat.None);

        public float Scale { get; private set; }

        public Point Size { get; private set; }

        public Point Offset { get; private set; }

        private Color Clear = Color.Wheat;

        private Rectangle Bounds = Rectangle.Empty;

        internal void Initialize()
        {
            var viewportSize = spriteBatch.GraphicsDevice.Viewport.Bounds.Size;

            var scale = viewportSize.ToVector2() / resolution.ToVector2();

            Scale = Math.Min(scale.X, scale.Y);

            Size = new Vector2(resolution.X * Scale, resolution.Y * Scale).ToPoint();

            Offset = (viewportSize - Size) / new Point(2);

            Bounds = new Rectangle(Offset, Size);
        }

        internal void Dispose() => renderTarget.Dispose();

        internal void BeginDraw()
        {
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.GraphicsDevice.Clear(Clear);

            spriteBatch.Begin(
                sortMode: SpriteSortMode.Deferred,
                blendState: BlendState.AlphaBlend,
                samplerState: SamplerState.PointClamp
            );
        }

        internal void EndDraw()
        {
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, Bounds, Color.White);
            spriteBatch.End();
        }
    }
}