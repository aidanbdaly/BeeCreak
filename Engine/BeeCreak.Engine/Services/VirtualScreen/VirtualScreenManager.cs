using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class VirtualScreenManager : IVirtualScreenService
    {
        private readonly App app;

        public VirtualScreen? Screen { get; private set; }

        public VirtualScreenManager(App app)
        {
            this.app = app;

            app.Services.AddService(this);
            app.Services.AddService<IVirtualScreenService>(this);
        }

        public void OnWindowResize()
        {
            Screen?.Initialize();
        }

        public Point ToVirtualScreenCoordinates(Point coordinate)
        {
            if (Screen == null)
            {
                return coordinate;
            }

            return (coordinate - Screen.Offset) / new Vector2(Screen.Scale).ToPoint();
        }

        public void CreateScreen(Point resolution)
        {
            Screen?.Dispose();

            Screen = new VirtualScreen(app.SpriteBatch, resolution);

            Screen.Initialize();
        }
    }
}