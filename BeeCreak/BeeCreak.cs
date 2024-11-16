namespace BeeCreak
{
    using System;
    using global::BeeCreak.Game;
    using global::BeeCreak.Menu;
    using global::BeeCreak.Tools.Dynamic;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BeeCreak : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly IInput input;
        private readonly ISound sound;
        private readonly IServiceProvider serviceProvider;
        private readonly IAppRouter appRouter;

        public BeeCreak(
            IServiceProvider serviceProvider,
            IAppRouter appRouter,
            IInput input,
            ISound sound)
        {
            this.serviceProvider = serviceProvider;
            this.appRouter = appRouter;
            this.input = input;
            this.sound = sound;

            graphics = new GraphicsDeviceManager(this);

            IsFixedTimeStep = false;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            graphics.SynchronizeWithVerticalRetrace = false;

            graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load content
            var sprite = serviceProvider.GetRequiredService<ISprite>();

            sprite.LoadContent(Content, GraphicsDevice);

            // App tree setup
            var gameNode = new AppNode();

            gameNode.Mode = serviceProvider.GetRequiredService<GameManager>();

            var mainMenuNode = new AppNode();
            var loadMenuNode = new AppNode();
            var settingsMenuNode = new AppNode();

            mainMenuNode.Mode = serviceProvider.GetRequiredService<Main>();
            loadMenuNode.Mode = serviceProvider.GetRequiredService<Load>();
            settingsMenuNode.Mode = serviceProvider.GetRequiredService<Settings>();

            mainMenuNode.SubNodes.Add("load", loadMenuNode);
            mainMenuNode.SubNodes.Add("settings", settingsMenuNode);

            var root = new AppNode();

            root.SubNodes.Add("game", gameNode);
            root.SubNodes.Add("mainMenu", mainMenuNode);

            appRouter.SetRoot(root);

            appRouter.Navigate("mainMenu");
        }

        protected override void Update(GameTime gameTime)
        {
            if (input.OnActionHold(InputAction.Exit))
            {
                Exit();
            }

            input.Update(gameTime);
            sound.Update(gameTime);

            appRouter.CurrentNode.Mode.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            appRouter.CurrentNode.Mode.Draw();

            base.Draw(gameTime);
        }
    }
}
