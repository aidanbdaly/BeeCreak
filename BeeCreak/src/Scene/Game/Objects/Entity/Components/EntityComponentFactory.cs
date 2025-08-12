using BeeCreak.Shared.Services;
using BeeCreak.src.Models;

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
            return new EntityComponent(entity, assetManager.Load<SpriteSheet>("Spritesheet/entities"));
        }
    }
}