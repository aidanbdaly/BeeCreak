using BeeCreak.Core.Input;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        protected SceneManager sceneManager;

        private SpriteBatch spriteBatch;

        private Context context;

        private Input input;

        public string StartScene { get; set; }

        public static readonly string AppDataDirectory =
          Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeeCreak");

        public Game()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            graphicsDeviceManager = new GraphicsDeviceManager(this);

            sceneManager = new SceneManager(
              contentManager: Content
          );
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager.RecomputeScaleUp();

            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
            }

            Exiting += (_, __) => sceneManager.UnloadScene();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            sceneManager.Initialize(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager.LoadScene(StartScene);
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
