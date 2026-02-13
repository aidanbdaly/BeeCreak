using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services
{
    public class ScreenService : IScreenService
    {
        private readonly App app;

        public Canvas? Canvas { get; private set; }

        public ScreenService(App app)
        {
            this.app = app;

            app.Services.AddService(this);
            app.Services.AddService<IScreenService>(this);
        }

        public void OnWindowResize()
        {
            Canvas?.Initialize();
        }

        public Point ToScreenCoordinates(Point coordinate)
        {
            if (Canvas == null)
            {
                return coordinate;
            }

            return (coordinate - Canvas.Offset) / new Vector2(Canvas.Scale).ToPoint();
        }

        public Rectangle Bounds()
        {
            if (Canvas != null)
            {
                return new Rectangle(0, 0, Canvas.OriginalSize.X, Canvas.OriginalSize.Y);
            }

            var viewport = app.GraphicsDevice.Viewport;
            return new Rectangle(0, 0, viewport.Width, viewport.Height);
        }

        public void SetRenderTarget(RenderTarget2D? renderTarget)
        {
            if (renderTarget != null)
            {
                app.GraphicsDevice.SetRenderTarget(renderTarget);
            }
            else if (Canvas != null)
            {
                app.GraphicsDevice.SetRenderTarget(Canvas.renderTarget);
            }
            else
            {
                app.GraphicsDevice.SetRenderTarget(null);
            }
        }

        public void CreateCanvas(Point resolution)
        {
            Canvas?.Dispose();

            Canvas = new Canvas(app.SpriteBatch, resolution);

            Canvas.Initialize();
        }
    }
}