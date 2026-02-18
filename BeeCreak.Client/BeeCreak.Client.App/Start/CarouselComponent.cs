using BeeCreak.Engine;
using Microsoft.Xna.Framework;

namespace BeeCreak.Intro
{
    public class CarouselComponent(App app) : DrawableGameComponent(app)
    {
        protected override void LoadContent()
        {
            var d = "The Gazers, drunk on aspiration provided by the boundless horizon wherever they should turn, lived a life consumed by their own stupor. For their sight had never been broken by the trunks of skyscrapers, leaving them free to orient themselves to any of lifes callings.";
        }
    }
}