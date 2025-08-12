
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class Scene : IScene
    {
        protected readonly List<IDrawable> components = [];

        protected readonly List<IBehavior> behaviors = [];

        private readonly IGraphicsDeviceService graphicsDeviceService;

        private readonly AssetManager assetManager;

        private readonly GameWindow gameWindow;

        public Scene(IGraphicsDeviceService graphicsDeviceService, AssetManager assetManager, GameWindow gameWindow)
        {
            this.graphicsDeviceService = graphicsDeviceService;
            this.assetManager = assetManager;
            this.gameWindow = gameWindow;
        }

        protected Color BackgroundColor { get; set; } = Color.Black;

        protected Transition EntranceTransition { get; set; } = Transition.None;

        protected Transition ExitTransition { get; set; } = Transition.None;

        private ITransition ActiveTransition { get; set; }

        public void Enter()
        {
            LoadContent(assetManager);
            Layout(gameWindow);

            if (EntranceTransition != Transition.None)
            {
                ActiveTransition = new FadeInTransition(graphicsDeviceService.GraphicsDevice, 3, () =>
                {
                    ActiveTransition = null;
                });

                ActiveTransition.Layout(gameWindow);
            }
        }

        public void Exit(Action onFinishedExiting)
        {
            if (ExitTransition != Transition.None)
            {
                ActiveTransition = new FadeOutTransition(graphicsDeviceService.GraphicsDevice, 3, () =>
                {
                    ActiveTransition = null;
                    UnloadContent(assetManager);
                    onFinishedExiting.Invoke();
                });

                ActiveTransition.Layout(gameWindow);
            }
            else
            {
                UnloadContent(assetManager);
                onFinishedExiting.Invoke();
            }
        }

        protected virtual void LoadContent(AssetManager assetManager) {}

        protected virtual void UnloadContent(AssetManager assetManager) {}

        public virtual void Layout(GameWindow gameWindow)
        {
            foreach (var component in components)
            {
                if (component is IResponsive responsive)
                {
                    responsive.Layout(gameWindow);
                }
            }
        }

        virtual public void Update(GameTime gameTime)
        {
            foreach (var updateable in behaviors)
            {
                updateable.Update(gameTime);
            }

            foreach (var component in components)
            {
                if (component is IBehavior updateable)
                {
                    updateable.Update(gameTime);
                }
            }

            if (ActiveTransition != null)
            {
                ActiveTransition.Update(gameTime);
            }
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDeviceService.GraphicsDevice.Clear(BackgroundColor);

            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }

            if (ActiveTransition != null)
            {
                ActiveTransition.Draw(spriteBatch);
            }
        }
    }
}