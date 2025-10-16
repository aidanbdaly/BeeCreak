using System.Threading.Tasks;
using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        protected readonly GraphicsDeviceManager graphicsDeviceManager;

        protected SpriteBatch spriteBatch;

        protected SceneManager sceneManager;

        protected readonly SceneFactory sceneFactory = new();

        protected readonly TransitionFactory transitionFactory = new();

        protected readonly AssetManager assetManager;

        protected string StartScene { get; set; }

        public static readonly string AppDataDirectory =
          Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeeCreak");

        public Game()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            graphicsDeviceManager = new GraphicsDeviceManager(this);
            assetManager = new AssetManager(Content);
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager?.OnWindowResize();

            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
            }

            Exiting += async (_, __) => await sceneManager.ExitSceneAsync(default);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            sceneManager = new SceneManager(
                sceneFactory,
                transitionFactory,
                GraphicsDevice,
                assetManager
                );

            base.Initialize();
        }

        protected override async void LoadContent()
        {
            await sceneManager.ChangeSceneAsync(StartScene);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);

            InputManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(spriteBatch);
        }
    }
}