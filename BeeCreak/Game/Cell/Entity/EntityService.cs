using BeeCreak.Game.Models;
using BeeCreak.Core.Components.Controllers;
using BeeCreak.Core.Input;
using BeeCreak.Core;
using BeeCreak.Core.Components;

namespace BeeCreak.Game.Domain.Entity
{
    // What is an entity? ("anything with behaviour?") Because this service could also be used to register a button. Strip it down to its core and put it in
    // BeeCreak core 

    public sealed class EntityHandle(TextureComponent component, Action onUnload) : IDisposable
    {
        public TextureComponent Component { get; init; } = component;

        public void Dispose() => onUnload();
    }

    public class EntityService(Scene scene, InputManager inputManager)
    {
        private readonly Scene scene = scene;

        private readonly AnimationService animationService = new(scene);

        private readonly EntityBehaviourFactory behaviourFactory = new(inputManager);

        public EntityHandle Load(EntityReference entity)
        {
            var animationHandle = animationService.Load(entity.Base.AnimationSheet, 1);
            entity.State.AnimationName = animationHandle.AnimationName;



            animationHandle.Component.Position = entity.State.Position;
        
            var behaviourHandles = entity.Base.Behaviours.Select(b =>
                scene.AddComponent(
                    behaviourFactory.Create(b, new(entity.Cell.TileMap, entity)))
            ).ToList();

            return new EntityHandle(
                animationHandle.Component,
                () =>
                {
                    behaviourHandles.ForEach(h => h.Dispose());
                    animationHandle.Dispose();
                }
            );
        }
    }
}