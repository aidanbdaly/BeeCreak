using BeeCreak.Core.Models;

namespace BeeCreak.Core.Components.Controllers
{
    public class AnimationFactory
    {
        public static Component Create(Animation animation, int secondsPerFrame, RenderableState state)
        {
            var component = new Component();

            var ticker = new Ticker(secondsPerFrame);
            var sprite = new Sprite(animation.SpriteSheet, animation.Data[0], state);

            var binding = ticker.BindOnTick((ticks) => sprite.SetSprite(animation.Data[ticks % animation.Data.Count]));

            component.AddRenderable(sprite);
            component.AddUpdateable(ticker);
            component.AddBinding(binding);

            return component;
        }
    }
}
