using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game;
using BeeCreak.Game.Intro;

namespace BeeCreak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using var app = new App();

                app.SceneFactory.RegisterService(app => new GameContext(app));

                app.SceneFactory.RegisterScene(
                    "IntroScene",
                    new SceneBuilder()
                        .AddComponent(app => new CarouselComponent(app))
                        .UseResolution(640, 360)
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    "MenuScene",
                    new SceneBuilder()
                        .UseResolution(640, 360)
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    "PlayScene",
                    new SceneBuilder()
                        .AddComponent(app => new DirectionalInputComponent(app))
                        .AddComponent(app => new CellComponent(app))
                        .UseResolution(800, 600)
                        .Build()
                );

                app.SceneFactory.SetStartScene("PlayScene");

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}