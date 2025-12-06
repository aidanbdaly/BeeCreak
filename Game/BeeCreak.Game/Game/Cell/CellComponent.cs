using BeeCreak.Engine;
using BeeCreak.Game.Cell;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game
{
    public class CellComponent(App app) : IGameComponent
    {
        private readonly CellManager cellManager = new(app);

        public void Initialize()
        {
            var context = app.Services.GetService<GameContext>()
                ?? throw new InvalidOperationException("GameContext service not found");

            var game = context.Game
                ?? throw new InvalidOperationException("Game not initialized in GameContext");

            cellManager.ChangeCell(game.CellReference);
        }
    }
}