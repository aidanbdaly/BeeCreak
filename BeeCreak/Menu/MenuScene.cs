using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework;

namespace BeeCreak.Menu.Scenes
{
    public class MenuScene : Scene
    {
        private readonly Context context;

        private readonly BaseComponentFactory factory;

        public MenuScene(Context context)
        {
            Width = 640;
            Height = 360;

            this.context = context;
            factory = new BaseComponentFactory(context);
        }

        public override void LoadContent()
        {
            var button = factory.Button(
                "Start Game",
                "buttons",
                "lookout",
                () => context.switchScene("PlayScene")
            );

            button.Position = new Vector2(Width / 2, Height / 2);

            AddComponent(button);
        }
    }
}
