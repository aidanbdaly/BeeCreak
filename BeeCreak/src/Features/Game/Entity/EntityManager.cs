namespace BeeCreak.Game.Scene.Entity
{
    using System;
    using System.Collections.Generic;
    using global::BeeCreak.Features.Game.Entity;
    using Microsoft.Xna.Framework;

    public class EntityManager
    {
        private readonly EntityAtlas entityAtlas;

        public EntityManager(EntityAtlas entityAtlas)
        {
            this.entityAtlas = entityAtlas;
        }

        private List<IEntity> Entities { get; set; }

        private List<(Action<Entity> action, Entity entity)> UpdateTasks { get; set; } = new List<(Action<Entity> action, Entity entity)>();

        public void SetEntities(List<IEntity> entities)
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
                entityAtlas.DrawEntity(entity);
            }
        }
    }
}
