namespace BeeCreak.Game
{
    using System;
    using global::BeeCreak.Events;
    using global::BeeCreak.Game.Scene;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;

    public class GameManager : IDynamicRenderable
    {
        private readonly Mode<RunMode> runMode;

        private readonly SaveManager saveManager;

        private readonly IToolCollection tools;

        private CellManager cellManager;

        private UIManager uIManager;

        public GameManager(IToolCollection tools, Mode<RunMode> runMode, SaveManager saveManager)
        {
            this.tools = tools;
            this.runMode = runMode;
            this.saveManager = saveManager;

            tools.Static.Events.Listen<NewGameEvent>(HandleNewGame);
            tools.Static.Events.Listen<LoadGameEvent>(HandleLoadGame);
        }

        public GameState GameState { get; set; }

        public void Update(GameTime gameTime)
        {
            if (tools.Dynamic.Input.OnActionClick(InputAction.Confirm))
            {
                Console.WriteLine("Saving");

                saveManager.Save(this.GameState);

                Console.WriteLine("Saved");
            }

            if (tools.Dynamic.Input.OnActionClick(InputAction.Open))
            {
                runMode.Switch(RunMode.MainMenu);
            }

            cellManager.Update(gameTime);
            uIManager.Update(gameTime);

            GameState.Time.Update(gameTime);
            GameState.Camera.Update(gameTime);
        }

        public void Draw()
        {
            cellManager.Draw(GameState.Camera);
            uIManager.Draw();
        }

        private void HandleNewGame(NewGameEvent e)
        {
            GameState gameState = saveManager.New();

            Initialize(gameState);
        }

        private void HandleLoadGame(LoadGameEvent e)
        {
            GameState gameState = saveManager.Load(e.Name);

            Initialize(gameState);
        }

        private void Initialize(GameState gameState)
        {
            GameState = gameState;

            cellManager = new CellManager(tools, GameState.ActiveCell);
            uIManager = new UIManager(tools, GameState.Time);

            runMode.Switch(RunMode.Game);
        }
    }
}
