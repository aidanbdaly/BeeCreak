using BeeCreak.Core;
using BeeCreak.Core.Services;

namespace BeeCreak.Menu
{
    public class MenuScene(App app) : Scene(app.GraphicsDevice, width, height)
    {
        private const int width = 640;

        private const int height = 360;

        private readonly App app = app;

        private readonly TransitionService transitionService = new(this);

        public override void LoadContent()
        {
            var context = app.Services.GetService<Core.AppContext>();

            var button = factory.Button(
                "Start Game",
                "menu_button",
                "lookout",
                async () =>
                {
                    try
                    {
                        await fadeout.PlayAsync(default)
                    }
                    finally
                    {
                        app.sceneManager.SwitchScene("IntroScene");
                    }
                }
            );

            button.Position = Size / 2;

            AddComponent(button);
        }
    }
}
