using BeeCreak.Core.Components;
using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game.Domain.Entity
{
    public enum EntityBehaviour
    {
        Control
    }

    public record EntityBehaviourContext
    (
        TileMapRecord TileMap,
        EntityReference Entity
    );

    public class EntityBehaviourFactory()
    {
        private readonly Dictionary<EntityBehaviour, Func<EntityBehaviourContext, Updateable>> factories = [];

        public Updateable Create(EntityBehaviour behaviour, EntityBehaviourContext context)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(context);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }

        public void Register(EntityBehaviour behaviour, Func<EntityBehaviourContext, Updateable> factory)
        {
            factories[behaviour] = factory;
        }
    }
}