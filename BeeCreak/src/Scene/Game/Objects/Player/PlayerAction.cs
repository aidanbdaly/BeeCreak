namespace BeeCreak.Shared.Services.Dynamic;

using Microsoft.Xna.Framework.Input;

public struct PlayerAction
{
    public Buttons Button;

    public Keys Key;

    public PlayerAction(Buttons buttons, Keys keys)
    {
        Button = buttons;
        Key = keys;
    }

    public static PlayerAction Up => new(Buttons.DPadUp, Keys.W);

    public static PlayerAction Down => new(Buttons.DPadDown, Keys.S);

    public static PlayerAction Left => new(Buttons.DPadLeft, Keys.A);

    public static PlayerAction Right => new(Buttons.DPadRight, Keys.D);

    public static PlayerAction Exit => new(Buttons.Back, Keys.Escape);

    public static PlayerAction Confirm => new(Buttons.A, Keys.Enter);

    public static PlayerAction Open => new(Buttons.X, Keys.E);
}