using BeeCreak.Engine.Core;
using BeeCreak.Engine.Transitions;
using BeeCreak.Intro;

namespace BeeCreak
{
    public class BeeCreak : Game
    {
        public BeeCreak() : base()
        {
            StartScene = "MenuScene";
        }

        protected async override void Initialize()
        {
            base.Initialize();

            sceneFactory.RegisterScene("MenuScene", (context, services) => new MenuScene(context, services));
            sceneFactory.RegisterScene("IntroScene", (context, services) => new IntroScene(context, services));
            sceneFactory.RegisterScene("GameScene", (context, services) => new GameScene(context, services));

            transitionFactory.RegisterTransition(Transition.FadeIn, duration => new FadeInTransition(GraphicsDevice, duration));
            transitionFactory.RegisterTransition(Transition.FadeOut, duration => new FadeOutTransition(GraphicsDevice, duration));
        }
    }
}

