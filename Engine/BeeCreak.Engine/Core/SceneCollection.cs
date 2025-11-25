namespace BeeCreak.Core
{
    public class SceneCollection
    {
        private readonly Dictionary<string, Func<IScene>> scenes = [];

        public void Register(string sceneId, Func<IScene> factory)
        {
            scenes[sceneId] = factory;
        }

        public IScene Get(string sceneId)
        {
            return scenes[sceneId]();
        }

        public IScene GetFirst()
        {
            return scenes[scenes.Keys.First()]();
        }
    }
}