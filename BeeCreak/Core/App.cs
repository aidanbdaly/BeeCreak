using System.Globalization;
using BeeCreak.Core.Input;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class App : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        private readonly SceneManager sceneManager;

        private readonly SceneCollection sceneCollection;

        public App()
        {
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";

            sceneCollection = new SceneCollection();

            graphicsDeviceManager = new GraphicsDeviceManager(this);

            sceneManager = new SceneManager(sceneCollection);

            Services.AddService(
                 new InputManager(sceneManager)
            );
        }

        public void RegisterScene(string Id, Func<Scene> factory)
        {
            sceneCollection.Register(Id, factory);
        }

        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphicsDeviceManager.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += (_, __) => sceneManager.RecomputeScaleUp();

            Exiting += (_, __) => sceneManager.UnloadScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Services.AddService(
                new AppContext(
                    Content.Load<Locale>($"Locales/{CultureInfo.CurrentCulture.Name}")
                )
            );

            sceneManager.Scene.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            sceneManager.Draw();

            base.Draw(gameTime);
        }
    }
}
