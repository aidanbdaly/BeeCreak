using BeeCreak.Engine;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Entity
{
    public enum EntityBehaviour
    {
        Control
    }

    public class EntityBehaviourFactory(App app)
    {
        private readonly Dictionary<EntityBehaviour, Func<EntityReference, IGameComponent>> factories = new()
        {
            { EntityBehaviour.Control, entity => new EntityControlComponent(app, entity) }
        };

        public IGameComponent Create(EntityBehaviour behaviour, EntityReference entity)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(entity);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }
    }
}