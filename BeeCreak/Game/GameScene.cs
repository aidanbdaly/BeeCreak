using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameScene : IScene
{
    private readonly TileLayerProvider tileLayer;

    private readonly PlayerSystem playerSystem;

    private readonly GameContext gameContext;

    private readonly SaveManager saveManager;

    private readonly CellLoader cellLoader;

    public GameScene
    (
        GameContext gameContext,
        SaveManager saveManager,
        CellLoader cellLoader,
        TileLayerProvider tileLayer,
        PlayerSystem playerSystem
    )
    {
        this.gameContext = gameContext;
        this.saveManager = saveManager;
        this.tileLayer = tileLayer;
        this.playerSystem = playerSystem;
        this.cellLoader = cellLoader;
    }

    public void LoadContent(AssetManager assetManager)
    {
        gameContext.Instance = saveManager.GetSave(gameContext.SaveId);

        cellLoader.MountCell(gameContext.Instance.ActiveCell);

        playerSystem.Initialize();
    }

    public void PerformLayout(GameWindow gameWindow)
    {
    }

    public void UnloadContent()
    {
        playerSystem.Dispose();
    }

    public void Dispose() => UnloadContent();

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}