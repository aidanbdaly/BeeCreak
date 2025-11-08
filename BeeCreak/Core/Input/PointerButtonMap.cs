namespace BeeCreak.Core.Input;

public enum PointerButton
{
    Left,
    Right,
    Middle
}

public struct PointerButtonMap(PointerButton button)
{
    public PointerButton Button = button;

    public static PointerButtonMap Left => new(PointerButton.Left);

    public static PointerButtonMap Right => new(PointerButton.Right);

    public static PointerButtonMap Middle => new(PointerButton.Middle);
}