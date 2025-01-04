namespace BeeCreak.Components.Button;

public struct ButtonVariant
{
    public ButtonVariant(int id)
    {
        Id = id;
    }

    public static ButtonVariant Default => new(0);
    public static ButtonVariant Hovered => new(1);

    public int Id { get; set; }
}