using BeeCreak.Core.Components;
using BeeCreak.Core.Input;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Domain.Entity
{
    public enum EntityBehaviour
    {
        Control
    }

    public class EntityBehaviourFactory(InputManager inputManager)
    {
        private readonly Dictionary<EntityBehaviour, Func<EntityReference, Updateable>> factories = new()
        {
            { EntityBehaviour.Control, entity => new ControlBehaviour(inputManager, entity) }
        };

        public Updateable Create(EntityBehaviour behaviour, EntityReference entity)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(entity);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }
    }
}