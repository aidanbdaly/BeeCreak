using BeeCreak.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        protected SceneManager sceneManager;

        private SpriteBatch spriteBatch;

        private Context context;

        public static readonly string AppDataDirectory =
          Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeeCreak");

        public Game()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager.OnWindowResized();

            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
            }

            Exiting += (_, __) => sceneManager.UnloadScene();

            spriteBatch = new SpriteBatch(GraphicsDevice);
         
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sceneManager.Startup();
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

            base.Draw(gameTime);
        }
    }
}
