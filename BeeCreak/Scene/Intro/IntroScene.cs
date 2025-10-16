using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.Intro
{
    public class IntroScene : Scene
    {
        public IntroScene(SceneContext context, SceneServices services)
        {
            EntranceTransition = Transition.FadeIn;

            Width = 640;
            Height = 360;

            Initialize(services);
        }

        private void Initialize(SceneServices services)
        {
            var textFactory = new TextFactory(services.AssetManager);

            var paragraph = textFactory.CreateParagraph(
                "The Gazers, inebriated on aspiration provided by the boundless horizon wherever they should turn, lived a life consumed by their own stupor. For their sight had never been broken by the trunks of skyscrapers, leaving them free to orient themselves to any of lifes callings.",
                200
            );

            paragraph.UpdateWorldTransform(
                new Transform(new Vector2(Width / 2, Height / 2), 0, 1)
            );

            components.Add(paragraph);
        }
    }
}