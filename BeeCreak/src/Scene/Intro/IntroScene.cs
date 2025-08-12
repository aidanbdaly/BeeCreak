using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class IntroScene : Scene
    {
        public IntroScene(
            IGraphicsDeviceService graphicsDeviceService,
            AssetManager assetManager,
            GameWindow gameWindow
            ) : base(graphicsDeviceService, assetManager, gameWindow)
        {
            EntranceTransition = Transition.FadeIn;

            components.Add(new IntroTextComponent());
        }
    }
}