using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Presentation;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class MenuScene : Scene
    {
        private readonly SceneManager sceneManager;

        public MenuScene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;

            EntranceTransition = Transition.FadeIn;
            ExitTransition = Transition.FadeOut;

            Width = 640;
            Height = 360;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            var menus = new List<CompositeMenu>
            {
                new("main-menu",
                [
                    CompositeMenuOption.Callback("Start", async () => await sceneManager.ChangeSceneAsync("IntroScene")),
                    CompositeMenuOption.Navigate("Options", "options-menu"),
                ], 16),
                new("options-menu",
                [
                    CompositeMenuOption.Navigate("Back", "main-menu")
                ], 16),
                new("load-menu",
                [
                    CompositeMenuOption.Navigate("Back", "main-menu")
                ], 16)
            };

            var buttonFactory = new ButtonFactory(assetManager);

            var mainMenu = new CompositeMenuComponent(buttonFactory, menus, "main-menu");

            mainMenu.UpdateWorldTransform(
                new Transform(new Vector2(Width / 2, Height / 2), 0, 1)
            );
            components.Add(mainMenu);
        }
    }
}

