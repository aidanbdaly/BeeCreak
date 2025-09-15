using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Utilities;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Core
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly AppContext appContext;

        protected readonly SceneManager sceneManager;

        protected readonly SceneFactory sceneFactory;

        protected readonly TransitionFactory transitionFactory;

        public Game()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            EngineContext.SetGraphicsDeviceManager(new GraphicsDeviceManager(this));

            var assetManager = new AssetManager(Content);

            var gameFactory = new GameFactory(assetManager);

            var userDataManager = new UserDataManager(gameFactory);

            appContext = new AppContext(userDataManager);

            sceneFactory = new(assetManager);
            transitionFactory = new();

            sceneManager = new SceneManager(sceneFactory, transitionFactory);
        }

        protected override void Initialize()
        {
            EngineContext.Initialize();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => VirtualDisplayManager.RecomputeScaleUp();

            Exiting += async (_, __) => await sceneManager.ExitSceneAsync(default);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            appContext.LoadUserSettings();

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
            VirtualDisplayManager.BeginVirtual(EngineContext.SpriteBatch);

            sceneManager.Draw(EngineContext.SpriteBatch);

            VirtualDisplayManager.EndVirtual(EngineContext.SpriteBatch);
        }
    }
}