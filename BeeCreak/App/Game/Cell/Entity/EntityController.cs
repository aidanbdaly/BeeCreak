using BeeCreak.App.Game.Models;
using BeeCreak.Core.Components.Controllers;

namespace BeeCreak.App.Game.Domain.Entity
{
    public class EntityController(
        EntityBehaviourFactory behaviourFactory,
        BehaviourController behaviours,
        AnimationController animations
        )
    {
        private readonly EntityBehaviourFactory behaviourFactory = behaviourFactory;

        private readonly BehaviourController behaviours = behaviours;

        private readonly AnimationController animations = animations;

        public void Mount(EntityReference entity)
        {
            entity.Base.Behaviours.ForEach(b =>
                behaviours.Mount(
                    $"entity_{entity.Id}_{b}",
                    behaviourFactory.Create(b, new(entity.Cell.TileMap, entity)))
            );

            entity.State.DirectionChanged += (direction) => DirectionChangedHandler(direction, entity);
        }

        public void Unmount(EntityReference entity)
        {
            entity.Base.Behaviours.ForEach(b =>
                behaviours.Unmount($"entity_{entity.Id}_{b}")
            );

            animations.Unmount($"entity_{entity.Id}");

            entity.State.DirectionChanged -= (direction) => DirectionChangedHandler(direction, entity);
        }

        private void DirectionChangedHandler(Direction direction, EntityReference entity)
        {
            animations.Unmount($"entity_{entity.Id}");

            var animation = entity.Base.AnimationSheet.GetAnimation(direction.ToString());

            animations.Mount(
                $"entity_{entity.Id}",
                animation,
                1);
        }
    }
}