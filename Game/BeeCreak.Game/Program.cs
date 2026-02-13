using BeeCreak.Engine;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Services;
using BeeCreak.Game;
using BeeCreak.Game.Cell;
using BeeCreak.Game.Home;
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
                    AppState.Menu,
                    new SceneBuilder()
                        .RegisterService<HomeContext>(app => new(app))
                        .SetCanvas(640, 360)
                        .SetRunAction((app) =>
                            app.Services.GetService<HomeContext>().Initialize())
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    AppState.Startup,
                    new SceneBuilder()
                        .SetCanvas(640, 360)
                        .Build()
                );

                app.SceneFactory.RegisterScene(
                    AppState.Playing,
                    new SceneBuilder()
                        .RegisterService<ISaveService, SaveService>(app => new(app))
                        .RegisterService<IEntityService, EntityService>(app => new(app))
                        .RegisterService<ICellService, CellManager>(app => new(app))
                        .SetCanvas(640, 360)
                        .SetRunAction(PlayContext.Initialize)
                        .Build()
                );

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