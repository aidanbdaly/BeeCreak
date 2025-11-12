using BeeCreak.Core.Components;
using BeeCreak.Core;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Intro
{
    public class IntroScene : Scene
    {
        private readonly Context context;

        public IntroScene(Context context)
        {
            Width = 640;
            Height = 360;

            this.context = context;
        }

        public override void LoadContent()
        {
            var paragraph = factory.Text("The Gazers, inebriated on aspiration provided by the boundless horizon wherever they should turn, lived a life consumed by their own stupor. For their sight had never been broken by the trunks of skyscrapers, leaving them free to orient themselves to any of lifes callings.", "lookout", 200);

            paragraph.Position = new Vector2(Width / 2, Height / 2);

            AddComponent(paragraph);
            AddComponent(
               factory.FadeInTransition(3)
           );
        }
    }
}
