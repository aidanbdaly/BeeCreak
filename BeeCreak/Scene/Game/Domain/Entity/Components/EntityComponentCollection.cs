using BeeCreak.Engine.Presentation.Composition;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class EntityComponentCollection : ComponentCollection<EntityComponent>
    {
        private readonly EntityManager entityManager;

        private readonly EntityComponentFactory componentFactory;

        private readonly Camera camera;

        public EntityComponentCollection(EntityManager entityManager, EntityComponentFactory componentFactory, Camera camera)
        {
            this.entityManager = entityManager;
            this.componentFactory = componentFactory;
            this.camera = camera;

            entityManager.StateImported += HandleStateImported;
        }

        private void HandleStateImported(object sender, EventArgs e)
        {
            foreach (var (id, entity) in entityManager.Entities)
            {
                components.Add(componentFactory.CreateEntityComponent(entity));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.Deferred,
                transformMatrix: camera.Transform
            );

            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
