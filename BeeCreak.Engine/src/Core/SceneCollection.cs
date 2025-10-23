namespace BeeCreak.Engine.Core
{
    public class SceneCollection
    {
        private readonly Dictionary<string, Func<Context, IScene>> scenes = [];

        public void Register(string sceneId, Func<Context, IScene> factory)
        {
            scenes[sceneId] = factory;
        }

        public IScene Get(Context services, string sceneId)
        {
            return scenes[sceneId](services);
        }
    }
}