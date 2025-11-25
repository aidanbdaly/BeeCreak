using BeeCreak.Core;
using BeeCreak.Core.Input;
using BeeCreak.Game.Cell;

namespace BeeCreak.Game
{
    public sealed class GameScene : Scene
    {
        private const int DefaultWidth = 800;

        private const int DefaultHeight = 600;

        private readonly App app;

        private readonly CellManager cellManager;

        public GameScene(App app) : base(app.GraphicsDevice, DefaultWidth, DefaultHeight)
        {
            this.app = app;

            cellManager = new CellManager(this, app.Services.GetService<InputManager>());
        }

        public override void LoadContent()
        {
            var game = app.Content.Load<GameRecord>("Game/default");

            cellManager.ChangeCell(game.ActiveCell);
        }
    }
}
