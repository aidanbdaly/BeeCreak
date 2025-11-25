using BeeCreak.Core;
using BeeCreak.Core.Services;

namespace BeeCreak.Menu
{
    public class MenuScene(App app) : Scene(app.GraphicsDevice, width, height)
    {
        private const int width = 640;

        private const int height = 360;

        private readonly TransitionService transitionService = new(this, app.GraphicsDevice);

        public override void LoadContent()
        {
            var context = app.Services.GetService<Core.AppContext>();

        

        }
    }
}
