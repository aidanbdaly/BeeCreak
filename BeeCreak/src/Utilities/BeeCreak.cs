using BeeCreak.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class BeeCreak : Microsoft.Xna.Framework.Game
    {
        private readonly IServiceProvider rootProvider;

        private SpriteBatch spriteBatch;

        private SceneManager sceneManager;

        private readonly GraphicsDeviceManager graphics;

        public BeeCreak(IServiceCollection services)
        {
            graphics = new GraphicsDeviceManager(this);

            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            services.AddSingleton<IGraphicsDeviceService>(graphics);
            services.AddSingleton(sp => new SpriteBatch(GraphicsDevice));
            services.AddSingleton(sp => new AssetManager(Content));
            services.AddSingleton(Window);

            rootProvider = services.BuildServiceProvider();
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            spriteBatch = rootProvider.GetRequiredService<SpriteBatch>();
            sceneManager = rootProvider.GetRequiredService<SceneManager>();
            var userDataManager = rootProvider.GetRequiredService<UserDataManager>();
            var appContext = rootProvider.GetRequiredService<AppContext>();

            appContext.SetUserSettings(userDataManager.LoadUserSettings());

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager.Scene?.Layout(Window);

            Exiting += (_, __) => sceneManager.Scene?.Exit(() => { });

            sceneManager.ChangeScene<MenuScene>();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.Scene?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Scene?.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}

