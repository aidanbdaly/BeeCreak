using BeeCreak.Core.Components;
using BeeCreak.Core;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Menu
{
    public class MenuScene : Scene
    {
        private readonly Context context;

        private readonly ComponentFactory factory;

        public MenuScene(Context context)
        {
            Width = 640;
            Height = 360;

            this.context = context;
            factory = new ComponentFactory(context);
        }

        public override void LoadContent()
        {
            var fadeout = factory.FadeOutTransition(3);

            var button = factory.Button(
                "Start Game",
                "menu_button",
                "lookout",
                async () =>
                {
                    try
                    {
                        await fadeout.PlayAsync(default);
                    }
                    finally
                    {
                        context.sceneManager.SwitchScene("IntroScene");
                    }
                }
            );

            button.Position = new Vector2(Width / 2, Height / 2);

            AddComponent(button);
            AddComponent(fadeout);
            AddComponent(
                factory.FadeInTransition(3)
            );
        }
    }
}
