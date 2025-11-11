using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class App : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        private readonly SceneManager sceneManager;

        protected readonly SceneCollection sceneCollection;

        private SpriteBatch? spriteBatch;

        public required string StartScene { get; set; }

        public static readonly string UserDataDirectory =
          Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Path.GetFileNameWithoutExtension(Environment.ProcessPath) ?? "bad");

        public App()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            sceneCollection = new SceneCollection();

            graphicsDeviceManager = new GraphicsDeviceManager(this);

            sceneManager = new SceneManager(
              contentManager: Content, 
              sceneCollection: sceneCollection
          );
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager.RecomputeScaleUp();

            if (!Directory.Exists(UserDataDirectory))
            {
                Directory.CreateDirectory(UserDataDirectory);
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
            if (spriteBatch is null)
            {
                throw new InvalidOperationException("SpriteBatch is not initialized.");
            }

            sceneManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
