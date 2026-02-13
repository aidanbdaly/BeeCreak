using BeeCreak.Engine.Core;

namespace BeeCreak.Engine.Services
{
    // This really is the factory for the entire game implementation
    public class SceneFactory
    {
        private readonly Dictionary<AppState, IScene> scenes = [];

        public void RegisterScene(AppState scene, IScene factory)
        {
            scenes[scene] = factory;
        }

        public IScene TryGetScene(AppState scene)
        {
            if (scenes.TryGetValue(scene, out var result))
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException($"Scene '{scene}' not found.");
            }
        }
    }
}