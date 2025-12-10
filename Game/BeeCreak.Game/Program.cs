using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game;
using BeeCreak.Game.Cell;
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

                app.SceneFactory.RegisterGlobalService(app => new GameContext(app));

                app.SceneFactory.RegisterScene(
                    "IntroScene",
                    new SceneBuilder()
                        .AddComponent(app => new CarouselComponent(app))
                        .SetResolution(640, 360)
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    "MenuScene",
                    new SceneBuilder()
                        .SetResolution(640, 360)
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    "PlayScene",
                    new SceneBuilder()
                        .RegisterService<ICellService, CellManager>(app => new(app))
                        .SetResolution(800, 600)
                        .SetOnBeginRun(app =>
                        {
                            var context = app.Services.GetService<GameContext>()
                                ?? throw new InvalidOperationException("GameContext service not found");

                            var game = context.Game;

                            var cellManager = app.Services.GetService<CellManager>();

                            cellManager.ChangeCell(game.CellReference);
                        })
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