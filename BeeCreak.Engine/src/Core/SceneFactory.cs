namespace BeeCreak.Engine.Core
{
    public class SceneFactory
    {
        private readonly Dictionary<string, Func<Context, SceneServices, IScene>> factories = [];

        public void RegisterScene(string sceneId, Func<SceneContext, SceneServices, IScene> factory)
        {
            factories[sceneId] = factory;
        }

        public IScene GetScene(SceneContext context, SceneServices services, string sceneId)
        {
            return factories[sceneId](context, services);
        }
    }
}