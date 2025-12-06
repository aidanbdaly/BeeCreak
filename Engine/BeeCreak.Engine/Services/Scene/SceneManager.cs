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

            foreach (var component in Scene.Components)
            {
                app.Components.Add(component(app));
            }
        }
    }
}