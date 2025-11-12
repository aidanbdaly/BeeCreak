namespace BeeCreak.Core.Components.Controllers
{
    public class BehaviourController(Scene scene)
    {
        private readonly Scene scene = scene;

        private readonly Dictionary<string, Updateable> behaviours = [];

        public void Mount(string Id, Updateable behaviour)
        {
            behaviours.Add(Id, behaviour);

            scene.AddComponent(behaviour);
        }

        public void Unmount(string Id)
        {
            if (behaviours.TryGetValue(Id, out var behaviour))
            {
                scene.RemoveComponent(behaviour);
                behaviours.Remove(Id);
            }
        }
    }
}