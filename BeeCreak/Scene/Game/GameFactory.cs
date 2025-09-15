using BeeCreak.Engine.Asset;

namespace BeeCreak
{
    public class GameFactory
    {
        private readonly AssetManager assetManager;
    
        public GameFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }
    
        public GameContext Create(string id, GameState state = null)
        {
            var blueprint = assetManager.Acquire<GameBlueprint>("BeeCreak");
    
            return new GameContext()
            {
                Id = id,
                State = state ?? blueprint.Asset.DefaultState,
                Blueprint = blueprint,
            };
        }
    }
}
