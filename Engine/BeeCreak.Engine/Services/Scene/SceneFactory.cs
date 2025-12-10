namespace BeeCreak.Engine.Services
{
    // This really is the factory for the entire game implementation
    public class SceneFactory
    {
        private readonly Dictionary<string, IScene> scenes = [];

        private readonly Dictionary<Type, Func<App, object>> services = [];

        private string startSceneId = string.Empty;

        public void SetStartScene(string sceneId)
        {
            startSceneId = sceneId;
        }

        public void RegisterGlobalService<TService>(Func<App, TService> factory) where TService : class
        {
            services[typeof(TService)] = app => factory(app);
        }

        public Dictionary<Type, Func<App, object>> GetRegisteredGlobalServices()
        {
            return services;
        }

        public void RegisterScene(string sceneId, IScene factory)
        {
            scenes[sceneId] = factory;
        }

        public IScene TryGetStartScene()
        {
            if (string.IsNullOrEmpty(startSceneId))
            {
                throw new InvalidOperationException("Start scene not set.");
            }

            return TryGetScene(startSceneId);
        }

        public IScene TryGetScene(string sceneId)
        {
            if (scenes.TryGetValue(sceneId, out var scene))
            {
                return scene;
            }
            else
            {
                throw new InvalidOperationException($"Scene '{sceneId}' not found.");
            }
        }
    }
}