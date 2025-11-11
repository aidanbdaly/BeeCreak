using BeeCreak.Core.Components;
using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game.Domain.Entity
{
    public enum Behaviour
    {
        Control
    }

    public record BehaviourContext
    (
        TileMapRecord TileMap,
        EntityReference Entity
    );

    public class BehaviourFactory()
    {
        private readonly Dictionary<Behaviour, Func<BehaviourContext, Updateable>> factories = [];

        public Updateable Create(Behaviour behaviour, BehaviourContext context)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(context);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }

        public void Register(Behaviour behaviour, Func<BehaviourContext, Updateable> factory)
        {
            factories[behaviour] = factory;
        }
    }
}