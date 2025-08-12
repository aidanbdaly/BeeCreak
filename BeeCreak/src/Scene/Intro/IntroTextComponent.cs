using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class IntroTextComponent : TextComponent, IResponsive
    {
        public IntroTextComponent() : base("The Gazers, inebriated by aspiration from the sight of the horizon wherever they should turn, lived a life consumed by their own stupor. For their sight had never been broken by the trunks of skyscrapers, and so their reality knew no limits. Unmoored from the heart they were free to orient themselves to any of lifes callings.", 200)
        {
            Scale = 4;
        }

        public void Layout(GameWindow gameWindow)
        {
            var frame = GetBounds();

            Position = new Vector2(gameWindow.ClientBounds.Width / 2, gameWindow.ClientBounds.Height / 2) - new Vector2(frame.Width / 2, frame.Height / 2);
        }
    }
}