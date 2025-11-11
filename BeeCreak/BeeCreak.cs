using BeeCreak.App.Intro;
using BeeCreak.App.Menu;
using BeeCreak.App.Game;

using CoreApp = BeeCreak.Core.App;

namespace BeeCreak
{
    public class BeeCreak : CoreApp
    {
        public BeeCreak()
        {
            StartScene = "MenuScene";
        }

        protected override void Initialize()
        {
            sceneCollection.Register("MenuScene", context => new MenuScene(context));
            sceneCollection.Register("IntroScene", context => new IntroScene(context));
            sceneCollection.Register("PlayScene", context => new GameScene(context));

            base.Initialize();
        }
    }
}
