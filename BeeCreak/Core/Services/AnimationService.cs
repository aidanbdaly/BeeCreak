using BeeCreak.Core.Models;
using BeeCreak.Core.State;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Components.Controllers
{
    public class AnimationFactory
    {
        // I don't think you should use animation sheet. Probably just animation
        public static Feature Create(AnimationSheet animationSheet, State<string> animationName, State<int> secondsPerFrame)
        {
            var ticker = new Ticker(secondsPerFrame.Value);

            var texture = new TextureComponent(animationSheet.Texture);

            var binding = texture.SourceRectangle.Listen(ticker.ticks, (ticks) => animationSheet.GetFrame(animationName.Value, ticks));



            return new Feature(texture, ticker);
        }
    }
}