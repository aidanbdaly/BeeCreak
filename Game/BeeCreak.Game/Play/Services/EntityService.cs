using BeeCreak.Engine;
using BeeCreak.Engine.Graphics;
using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Cell
{
    public interface IEntityService
    {
        void Spawn(EntityReference entity);
    }

    public class EntityService(App app) : IEntityService
    {
        private readonly EntityBehaviourFactory behaviourFactory = new(app);

        public void Spawn(EntityReference entity)
        {
            var animation = new AnimationComponent(app, entity.State.Animation, entity.State.CrackLevel)
            {
                Position = entity.State.Position
            };

            app.Components.Add(animation);

            entity.Base.Behaviours.ForEach(b => app.Components.Add(behaviourFactory.Create(b, entity)));
        }
    }
}
