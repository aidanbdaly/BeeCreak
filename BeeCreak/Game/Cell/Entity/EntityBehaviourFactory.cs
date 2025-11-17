using BeeCreak.Core.Components;
using BeeCreak.Core.Input;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Domain.Entity
{
    public enum EntityBehaviour
    {
        Control
    }

    public record EntityBehaviourContext
    (
        TileMap TileMap,
        EntityReference Entity
    );

    public class EntityBehaviourFactory(InputManager inputManager)
    {
        private readonly Dictionary<EntityBehaviour, Func<EntityBehaviourContext, Updateable>> factories = new()
        {
            { EntityBehaviour.Control, ctx => new ControlBehaviour(inputManager, ctx.TileMap, ctx.Entity) }
        };

        public Updateable Create(EntityBehaviour behaviour, EntityBehaviourContext context)
        {
            if (factories.TryGetValue(behaviour, out var factory))
            {
                return factory(context);
            }

            throw new ArgumentException($"No factory registered for behaviour {behaviour}");
        }
    }
}