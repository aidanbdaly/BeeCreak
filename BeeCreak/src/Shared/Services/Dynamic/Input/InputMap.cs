namespace BeeCreak.Shared.Services.Dynamic;

using Microsoft.Xna.Framework.Input;

public struct InputMap
{
    public Buttons? Buttons;
    public Keys? Keys;

    public InputMap(Buttons buttons, Keys keys)
    {
        Buttons = buttons;
        Keys = keys;
    }
}

public class InputAction
{
    public static readonly InputMap Up = new(Buttons.DPadUp, Keys.W);
    public static readonly InputMap Down = new(Buttons.DPadDown, Keys.S);
    public static readonly InputMap Left = new(Buttons.DPadLeft, Keys.A);
    public static readonly InputMap Right = new(Buttons.DPadRight, Keys.D);
    public static readonly InputMap Exit = new(Buttons.Back, Keys.Escape);
    public static readonly InputMap Confirm = new(Buttons.A, Keys.Enter);
    public static readonly InputMap Open = new(Buttons.X, Keys.E);
}
