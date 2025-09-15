using BeeCreak.Engine.Core;
using BeeCreak.Engine.Transitions;
using BeeCreak.Intro;

namespace BeeCreak
{
    public class BeeCreak : Game
    {
        protected async override void LoadContent()
        {
            base.LoadContent();

            sceneFactory.RegisterScene("MenuScene", sceneManager => new MenuScene(sceneManager));
            sceneFactory.RegisterScene("IntroScene", sceneManager => new IntroScene(sceneManager));
            sceneFactory.RegisterScene("GameScene", sceneManager => new GameScene(sceneManager));

            transitionFactory.RegisterTransition(Transition.FadeIn, duration => new FadeInTransition(EngineContext.GraphicsDevice, duration));
            transitionFactory.RegisterTransition(Transition.FadeOut, duration => new FadeOutTransition(EngineContext.GraphicsDevice, duration));

            await sceneManager.ChangeSceneAsync("MenuScene");
        }
    }
}

