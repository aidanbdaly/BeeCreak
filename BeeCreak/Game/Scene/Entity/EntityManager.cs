namespace BeeCreak.Game.Scene.Entity
{
    using System;
    using System.Collections.Generic;
    using global::BeeCreak.Game.Scene.Entity.Events;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;

    public class EntityManager
    {
        public EntityManager(IEventManager eventManager)
        {
            UpdateTasks = new List<(Action<Entity> action, Entity entity)>();

            eventManager.Listen<AddEntityEvent>(HandleAddEntity);
            eventManager.Listen<RemoveEntityEvent>(HandleRemoveEntity);
        }

        private List<Entity> Entities { get; set; }

        private List<(Action<Entity> action, Entity entity)> UpdateTasks { get; set; }

        public void SetEntities(List<Entity> entities)
        {
            Entities = entities;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var (action, entity) in UpdateTasks)
            {
                action.Invoke(entity);
            }

            UpdateTasks.Clear();

            foreach (var entity in Entities)
            {
                entity.Update(gameTime);
            }
        }

        public void Draw()
        {
            foreach (var entity in Entities)
            {
                entity.Draw();
            }
        }

        private void HandleAddEntity(AddEntityEvent addEntityEvent)
        {
            var entity = addEntityEvent.Entity;

            UpdateTasks.Add(((entity) => Entities.Add(entity), entity));
        }

        private void HandleRemoveEntity(RemoveEntityEvent removeEntityEvent)
        {
            var entity = removeEntityEvent.Entity;

            UpdateTasks.Add(((entity) => Entities.Remove(entity), entity));
        }
    }
}
