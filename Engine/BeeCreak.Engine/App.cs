using BeeCreak.Engine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine
{
    public class App : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        private readonly VirtualScreenManager virtualScreenManager;

        public SceneManager SceneManager
        {
            get
            {
                return Services.GetService<SceneManager>()
                ?? throw new InvalidOperationException("No scene service");
            }
        }

        public SceneFactory SceneFactory
        {
            get
            {
                return Services.GetService<SceneFactory>()
                ?? throw new InvalidOperationException("No scene factory");
            }
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
                return Services.GetService<SpriteBatch>()
                ?? throw new InvalidOperationException("No spritebatch");
            }
        }

        public App()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            Services.AddService(
                new SceneFactory()
            );

            virtualScreenManager = new VirtualScreenManager(this);
            graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => virtualScreenManager.OnWindowResize();

            Exiting += (_, __) => { };

            Services.AddService(
              new SceneManager(this)
            );

            Services.AddService(
                new TranslationService(this)
            );

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Services.AddService(new SpriteBatch(GraphicsDevice));
        }

        protected override void BeginRun()
        {
            var services = SceneFactory.GetRegisteredServices();

            foreach (var service in services)
            {
                Services.AddService(service.Key, service.Value(this));
            }

            SceneManager.StageFirst();
            SceneManager.Reveal();

            if (SceneManager.Scene.Resolution != Point.Zero)
            {
                virtualScreenManager.CreateScreen(SceneManager.Scene.Resolution);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            var screen = virtualScreenManager.Screen;

            if (screen is not null)
            {
                screen.BeginDraw();

                base.Draw(gameTime);

                screen.EndDraw();
            }
            else
            {
                base.Draw(gameTime);
            }
        }
    }
}
