namespace BeeCreak
{
    using System.Collections.Generic;
    using global::BeeCreak.Game;
    using global::BeeCreak.Menu;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Dynamic;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BeeCreak : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphics;
        private IToolCollection tools;
        private SaveManager saveManager;
        private Dictionary<RunMode, IDynamicRenderable> modes;
        private Mode<RunMode> mode;

        public BeeCreak()
        {
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

            IStaticToolCollection staticTools = new StaticTools
            {
                GraphicsDevice = GraphicsDevice,
                Sprite = new Sprite(Content, GraphicsDevice),
                Events = new EventManager(),
                TILE_SIZE = 32,
            };

            IDynamicToolCollection dynamicTools = new DynamicTools
            {
                Input = new Input(),
                Sound = new Sound(),
            };

            tools = new ToolCollection { Static = staticTools, Dynamic = dynamicTools, };
            saveManager = new SaveManager(tools);

            mode = new Mode<RunMode>(RunMode.MainMenu);

            modes = new Dictionary<RunMode, IDynamicRenderable>
            {
                { RunMode.MainMenu, new MenuManager(tools, mode, saveManager) },
                { RunMode.Game, new GameManager(tools, mode, saveManager) },
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Input.OnActionHold(InputAction.Exit))
            {
                Exit();
            }

            modes[mode.Current].Update(gameTime);
            tools.Dynamic.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            modes[mode.Current].Draw();
            base.Draw(gameTime);
        }
    }
}
