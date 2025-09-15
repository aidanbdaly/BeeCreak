using BeeCreak.Engine.Asset;

namespace BeeCreak
{
    public class EntityComponentFactory
    {

        private readonly AssetManager assetManager;

        public EntityComponentFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public EntityComponent CreateEntityComponent(Entity entity)
        {
            return new EntityComponent(entity, assetManager.Acquire<SpriteSheet>("Spritesheet/entities"));
        }
    }
}