namespace BeeCreak.Game
{
    using System;
    using global::BeeCreak.Events;
    using global::BeeCreak.Game.Scene;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Xna.Framework;

    public class GameManager : IGameObject
    {

        private readonly ISaveManager saveManager;

        private readonly IInput input;

        private CellManager cellManager;

        private UIManager uiManager;

        private IAppRouter appRouter;

        private IServiceProvider serviceProvider;

        public GameManager(IEventManager events, IInput input, IAppRouter appRouter, ISaveManager saveManager, IServiceProvider serviceProvider)
        {
            this.input = input;
            this.appRouter = appRouter;
            this.saveManager = saveManager;
            this.serviceProvider = serviceProvider;

            events.Listen<NewGameEvent>(HandleNewGame);
            events.Listen<LoadGameEvent>(HandleLoadGame);
        }

        public Game Game { get; set; }

        public void Update(GameTime gameTime)
        {
            if (input.OnActionClick(InputAction.Confirm))
            {
                Console.WriteLine("Saving");

                saveManager.Save(Game);

                Console.WriteLine("Saved");
            }

            cellManager.Update(gameTime);
            uiManager.Update(gameTime);

            Game.Time.Update(gameTime);
            Game.Camera.Update(gameTime);
        }

        public void Draw()
        {
            cellManager.Draw(Game.Camera);
            uiManager.Draw();
        }

        private void HandleNewGame(NewGameEvent e)
        {
            Initialize(saveManager.New());
        }

        private void HandleLoadGame(LoadGameEvent e)
        {
            Initialize(saveManager.Load(e.Name));
        }

        private void Initialize(Game game)
        {
            Game = game;

            cellManager = serviceProvider.GetRequiredService<CellManager>();
            uiManager = serviceProvider.GetRequiredService<UIManager>();

            cellManager.SetActiveCell(Game.ActiveCell);
            uiManager.SetTime(Game.Time);

            appRouter.Navigate("game");
        }
    }
}
