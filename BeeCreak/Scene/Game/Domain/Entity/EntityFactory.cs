using BeeCreak.Engine.Asset;
using BeeCreak.Shared.Services;
using BeeCreak.src.Models;

namespace BeeCreak
{   
    public class EntityFactory
    {
        private readonly AssetManager assetManager;
    
        public EntityFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }
    
        public Entity CreateEntity(EntityState state)
        {
            return new Entity(state, assetManager.Acquire<EntityAttributes>($"Entity/{state.ContentId}"));
        }
    }
}
