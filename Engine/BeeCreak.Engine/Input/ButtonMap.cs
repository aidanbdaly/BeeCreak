using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Input
{
    public struct ButtonMap(Buttons button, Keys key)
    {
        public Buttons Button = button;

        public Keys Key = key;

        public static ButtonMap Up => new(Buttons.DPadUp, Keys.W);

        public static ButtonMap Down => new(Buttons.DPadDown, Keys.S);

        public static ButtonMap Left => new(Buttons.DPadLeft, Keys.A);

        public static ButtonMap Right => new(Buttons.DPadRight, Keys.D);

        public static ButtonMap Exit => new(Buttons.Back, Keys.Escape);

        public static ButtonMap Confirm => new(Buttons.A, Keys.Enter);

        public static ButtonMap Open => new(Buttons.X, Keys.E);
    }
}