using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class MenuScene : Scene
    {
        private readonly BaseComponentFactory componentFactory;

        public MenuScene(
            IGraphicsDeviceService graphicsDeviceService,
            AssetManager assetManager,
            GameWindow gameWindow,
            BaseComponentFactory componentFactory
            ) : base(graphicsDeviceService, assetManager, gameWindow)
        {
            this.componentFactory = componentFactory;

            EntranceTransition = Transition.FadeIn;
            ExitTransition = Transition.FadeOut;
        }

        protected override void LoadContent(AssetManager assetManager)
        {
            components.Add(componentFactory.CreateBackground("Images/menu-background"));
            components.Add(componentFactory.CreateMainMenu());
        }
    }
}
