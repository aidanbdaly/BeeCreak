using BeeCreak.Shared.Services;

namespace BeeCreak
{
    
    public class GameFactory
    {
        private readonly AssetManager assetManager;
    
        public GameFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }
    
        public Game Create(string id, GameState state = null)
        {
            var blueprint = assetManager.Load<GameBlueprint>("BeeCreak");
    
            return new Game()
            {
                Id = id,
                State = state ?? blueprint.DefaultState,
                Blueprint = blueprint,
            };
        }
    }
}
