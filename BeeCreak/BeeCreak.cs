using BeeCreak.Core;
using BeeCreak.App.Intro;
using BeeCreak.App.Menu;
using BeeCreak.App.Game;

namespace BeeCreak
{
    public class BeeCreak : Game
    {
        public BeeCreak()
        {
            StartScene = "MenuScene";    
        }

        protected override void Initialize()
        {
            sceneManager.Scenes.Register("MenuScene", context => new MenuScene(context));
            sceneManager.Scenes.Register("IntroScene", context => new IntroScene(context));
            sceneManager.Scenes.Register("PlayScene", context => new GameScene(context));

            base.Initialize();
        }
    }
}
