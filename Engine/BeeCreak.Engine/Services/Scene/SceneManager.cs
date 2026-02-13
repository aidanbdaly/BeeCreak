using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface ISceneService
    {
        IScene Scene { get; }

        void Stage(AppState scene);

        void Reveal();
    }

    public class SceneManager(App app) : ISceneService 
    {
        public IScene Scene { get; private set; } = app.SceneFactory.TryGetScene(default!)
            ?? throw new InvalidOperationException("Default scene not found.");

        public void Stage(AppState scene)
        {
            Scene = app.SceneFactory.TryGetScene(scene);
        }

        public void Reveal()
        {
            app.Components.Clear();

            if (Scene.CanvasSize != Point.Zero)
            {
                app.Services.GetService<ScreenService>().CreateCanvas(Scene.CanvasSize);
            }

            foreach (var service in Scene.Services)
            {
                app.Services.AddService(
                    service.Key,
                    service.Value(app)
                );
            }

            Scene.OnBeginRun(app);
        }
    }
}