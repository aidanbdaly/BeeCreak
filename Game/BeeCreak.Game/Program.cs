using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game;
using BeeCreak.Game.Cell;
using BeeCreak.Game.Home;
using BeeCreak.Game.Home.Services;
using BeeCreak.Game.Intro;
using BeeCreak.Game.Play;
using BeeCreak.Game.Services;

namespace BeeCreak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using var app = new App();

                app.Services.AddService(new GameContext(app));

                app.SceneFactory.RegisterScene(
                    "Intro",
                    new SceneBuilder()
                        .AddComponent(app => new CarouselComponent(app))
                        .ConfigureCanvas(640, 360)
                        .Build()
                );

                // probably set controller / context
                app.SceneFactory.RegisterScene(
                    "Home",
                    new SceneBuilder()
                        .RegisterService<HomeContext>(app => new(app))
                        .RegisterService<MenuFactory>(app => new(app))
                        .ConfigureCanvas(640, 360)
                        .SetRunAction((app) =>
                            app.Services.GetService<HomeContext>().Initialize())
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    "Play",
                    new SceneBuilder()
                        .RegisterService<ISaveService, SaveService>(app => new(app))
                        .RegisterService<IEntityService, EntityService>(app => new(app))
                        .RegisterService<ICellService, CellManager>(app => new(app))
                        .ConfigureCanvas(640, 360)
                        .SetRunAction(PlayContext.Initialize)
                        .Build()
                );

                app.SceneFactory.SetStartScene("Play");

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