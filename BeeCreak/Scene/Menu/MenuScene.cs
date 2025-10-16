using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class MenuScene : Scene
    {
        private readonly SceneContext context;
        
        private readonly SceneServices services;

        public MenuScene(SceneContext context, SceneServices services)
        {
            this.context = context;
            this.services = services;

            EntranceTransition = Transition.FadeIn;
            ExitTransition = Transition.FadeOut;

            Width = 640;
            Height = 360;

            Initialize();
        }

        private void Initialize()
        {
            var menus = new List<CompositeMenu>
            {
                new("main-menu",
                [
                    CompositeMenuOption.Callback("Start", async () => await context.ChangeScene("IntroScene", default)),
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

            var buttonFactory = new ButtonFactory(services.AssetManager, context);

            var mainMenu = new CompositeMenuComponent(buttonFactory, menus, "main-menu");

            mainMenu.UpdateLocalTransform(
                new Vector2(Width / 2, Height / 2), 0, 1
            );

            components.Add(mainMenu);
        }
    }
}

