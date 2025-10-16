using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;

namespace BeeCreak
{
    public class GameScene : Scene
    {
        private readonly Context context;

        private readonly PlayContext playContext;

        private readonly EntityFactory entityFactory = new();

        public GameScene(Context context, SceneServices services)
        {
            this.context = context;
        }

        public void Initialize()
        {
            entityFactory.RegisterEntity(Entity.Player, props => new PlayerEntity(props));
        }

        public void LoadContent()
        {
            playContext.SaveState = SaveManager.Load(context.SaveId);

            var tileComponentList = playContext.SaveState.ActiveCell.TileMap.Select(tileState => new SpriteComponent(services.AssetManager.Acquire<SpriteSheet>(ContentPaths.SpriteSheetTiles), tileState.ContentId));

            var tileMap = new CachedComponentCollection<SpriteComponent>(services.GraphicsDevice, tileComponentList);
        }
    }
}