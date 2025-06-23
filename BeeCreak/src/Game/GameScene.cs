using BeeCreak.Scene.Main;
using BeeCreak.Shared.Services;

public class GameScene : Scene
{
    private readonly Game game;

    private readonly GameManager gameManager = new();

    public GameScene
    (
        Game game,
        CellManager cellManager,
        TileManager tileManager,
        EntityManager entityManager,
        PlayerBehavior playerSystem,
        Camera camera,
        TileVariator tileVariantSystem,
        TileLayerComponent tileLayerComponent,
        EntityLayerComponent entityLayerComponent
    )
    {
        this.game = game;

        components.Add(tileLayerComponent);
        components.Add(entityLayerComponent);

        behaviors.Add(playerSystem);
        behaviors.Add(camera);

    }

    public new void LoadContent(AssetManager assetManager)
    {
        base.LoadContent(assetManager);

        game.State = gameManager.LoadOrCreate(game.Id);
    }
} 



// public void LoadContent(AssetManager assetManager)
// {

//     var features = new List<DungeonFeature>()
//         {
//             new CircleFeature(9),
//             new RectangleFeature(5, 10),
//             new CircleFeature(7),
//             new RectangleFeature(3, 5),
//         };

//     var tileMapRaw = new DungeonGenerator(features).Route(300);




// }