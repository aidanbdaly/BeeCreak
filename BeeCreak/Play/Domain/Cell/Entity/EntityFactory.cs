using BeeCreak.Engine.Assets;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public class EntityFactory(AssetManager assetManager)
    {
        private readonly AssetManager assetManager = assetManager;

        private readonly Dictionary<Entity, Func<EntityProps, IEntity>> factories = [];

        public void RegisterEntity(Entity entity, Func<EntityProps, IEntity> factory)
        {
            factories[entity] = factory;
        }

        public IEntity CreateEntity(Entity type, EntityState state)
        {
            if (factories.TryGetValue(type, out var factory))
            {
                return factory(
                    new EntityProps(
                        type,
                        state,
                        assetManager.Acquire<SpriteSheet>($"entities/{type}"),
                        assetManager.Acquire<EntityAttributes>($"entities/{type}"
                        )
                    )
                );
            }

            throw new ArgumentException($"No factory registered for entity type {type}", nameof(state));
        }
    }
}
