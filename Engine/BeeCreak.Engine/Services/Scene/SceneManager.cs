using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface ISceneService
    {
        IScene Scene { get; }

        void Stage(string sceneId);

        void Reveal();
    }

    public class SceneManager(App app) : ISceneService
    {
        public IScene Scene { get; private set; } = app.SceneFactory.TryGetStartScene();

        public void StageFirst()
        {
            Scene = app.SceneFactory.TryGetStartScene();
        }

        public void Stage(string sceneId)
        {
            Scene = app.SceneFactory.TryGetScene(sceneId);
        }

        public void Reveal()
        {
            app.Components.Clear();

            foreach (var service in Scene.Services)
            {
                app.Services.AddService(
                    service.Key,
                    service.Value(app)
                );
            }

            foreach (var component in Scene.Components)
            {
                app.Components.Add(component(app));
            }

            if (Scene.Resolution != Point.Zero)
            {
                app.Services.GetService<VirtualScreenManager>().CreateScreen(Scene.Resolution);
            }

            Scene.OnBeginRun(app);
        }
    }
}