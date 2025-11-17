using BeeCreak.Core;

namespace BeeCreak.Intro
{
    public class IntroScene(App app) : Scene(app.GraphicsDevice, width, height)
    {
        private const int width = 640;

        private const int height = 360;

        private readonly App app = app;

        public override void LoadContent()
        {
            var context = app.Services.GetService<Core.AppContext>();

            var text = context.Locale.GetTranslation("");

            var paragraph = factory.Text("The Gazers, drunk on aspiration provided by the boundless horizon wherever they should turn, lived a life consumed by their own stupor. For their sight had never been broken by the trunks of skyscrapers, leaving them free to orient themselves to any of lifes callings.", "lookout", 200);

            paragraph.Position = Size / 2;

            AddComponent(paragraph);
        }
    }
}
