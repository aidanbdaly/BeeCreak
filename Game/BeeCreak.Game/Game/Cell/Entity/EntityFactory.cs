using BeeCreak.Game.Models;
using BeeCreak.Core.Components.Controllers;
using BeeCreak.Core.Input;
using BeeCreak.Core.Components;

namespace BeeCreak.Game.Domain.Entity
{
    // What is an entity? ("anything with behaviour?") Because this service could also be used to register a button. Strip it down to its core and put it in
    // BeeCreak core 

    // ComponentFactory takes RenderableFactory and UpdateableFactory 
    // You also somehow need to then be able to bind state

    public class EntityFactory(InputManager inputManager)
    {
        private readonly EntityBehaviourFactory behaviourFactory = new(inputManager);

        public Component Create(EntityReference entity)
        {
            var component = AnimationFactory.Create(
                entity.Base.Animation,
                entity.State.AnimationName,
                1,
                new(entity.State.Position));

            entity.Base.Behaviours.ForEach(b => component.AddUpdateable(behaviourFactory.Create(b, entity)));

            return component;
        }
    }
}
