using BeeCreak.Engine.Asset;

namespace BeeCreak.Engine.Core
{
    public class SceneFactory
    {
        private readonly Dictionary<string, Func<SceneManager, IScene>> factories = [];

        private readonly AssetManager assetManager;

        public SceneFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public void RegisterScene(string sceneId, Func<SceneManager, IScene> factory)
        {
            factories[sceneId] = factory;
        }

        public IScene GetScene(SceneManager sceneManager, string sceneId)
        {
            var scene = factories[sceneId](sceneManager);

            scene.LoadContent(assetManager);

            return scene;
        }
    }
}