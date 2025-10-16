using BeeCreak.Engine.Assets;

namespace BeeCreak
{
    public class EntityFactory
    {
        private readonly Dictionary<Entity, Func<EntityProps, IEntity>> factories = new();

        public void RegisterEntity(Entity entity, Func<EntityProps, IEntity> factory)
        {
            factories[entity] = factory;
        }

        public Entity CreateEntity(EntityProps state)
        {
            if (factories.TryGetValue(state.State.Type, out var factory))
            {
                return factory(state);
            }

            throw new ArgumentException($"No factory registered for entity type {state.State.Type}", nameof(state));
        }
    }
}
