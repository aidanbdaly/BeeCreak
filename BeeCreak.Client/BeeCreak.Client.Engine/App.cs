using BeeCreak.Engine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine
{
    public class App : Game
    {
        // You made it private readonly, there's not much point since this can be fetched anywhere
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        private readonly ScreenService screenService;

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

        public IScreenService ScreenService
        {
            get
            {
                return Services.GetService<IScreenService>()
                ?? throw new InvalidOperationException("No virtual screen service");
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

        public IMouseInputService Mouse
        {
            get
            {
                return mouse;
            }
        }

        private readonly MouseInputService mouse;

        private readonly KeyboardInputService keyboard;

        public App()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            Services.AddService(
                new SceneFactory()
            );

            screenService = new ScreenService(this);
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            mouse = new MouseInputService(this);
            keyboard = new KeyboardInputService(this);

            Services.AddService(mouse);
            Services.AddService(keyboard);
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => screenService.OnWindowResize();

            Exiting += (_, __) => { };

            Services.AddService(new SceneManager(this));
            Services.AddService(new TranslationService(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Services.AddService(new SpriteBatch(GraphicsDevice));
        }

        protected override void BeginRun()
        {
            SceneManager.Reveal();
        }

        protected override void Update(GameTime gameTime)
        {
            mouse.Update();
            keyboard.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var canvas = screenService.Canvas;

            if (canvas is not null)
            {
                canvas.BeginDraw();

                base.Draw(gameTime);

                canvas.EndDraw();
            }
            else
            {
                base.Draw(gameTime);
            }
        }
    }
}
