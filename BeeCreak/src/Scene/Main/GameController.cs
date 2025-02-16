using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class GameController : IGameController
{
    private readonly CellProvider cellProvider;

    private readonly CellController cellController;

    private readonly HUDController HUDController;

    public GameController(CellController cellController, HUDController uiManager)
    {
        this.cellController = cellController;
        this.HUDController = uiManager;
    }

    public void Load(Shared.Data.Models.Game game)
    {
        cellController.Load(cellProvider.GetCell(game.ActiveCell));
        HUDController.Load(game.Time);
    }

    public void Update(GameTime gameTime)
    {
        cellController.Update(gameTime);
        HUDController.Update(gameTime);
    }

    public void Draw()
    {
        cellController.Draw();
        HUDController.Draw();
    }
}
