using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class GameScene : Scene
    {
        public GameScene
        (
            IGraphicsDeviceService graphicsDeviceService,
            AssetManager assetManager,
            GameWindow gameWindow,


            CellManager cellManager,
            TileManager tileManager,
            EntityManager entityManager,
            PlayerBehavior playerSystem,
            Camera camera,
            TileVariator tileVariantSystem,
            TileMapComponent tileMapComponent,
            EntityComponentCollection entityComponentCollection
        ) : base(graphicsDeviceService, assetManager, gameWindow)
        {
            components.Add(tileMapComponent);
            components.Add(entityComponentCollection);

            behaviors.Add(playerSystem);
            behaviors.Add(camera);
        }
    }
}
