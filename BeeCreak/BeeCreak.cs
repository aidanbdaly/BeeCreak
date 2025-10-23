using BeeCreak.Engine.Core;
using BeeCreak.Intro.Scenes;
using BeeCreak.Menu.Scenes;
using BeeCreak.Play.Scenes;

namespace BeeCreak
{
    public class BeeCreak : Game
    {
        protected override void Initialize()
        {
            sceneManager.Scenes.Register("MenuScene", context => new MenuScene(context));
            sceneManager.Scenes.Register("IntroScene", context => new IntroScene(context));
            sceneManager.Scenes.Register("PlayScene", context => new PlayScene(context));

            base.Initialize();
        }
    }
}
