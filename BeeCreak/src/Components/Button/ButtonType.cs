namespace BeeCreak.Components.Button;

public struct ButtonType
{
    public ButtonType(int id)
    {
        Id = id;
    }

    public static ButtonType Default => new(0);

    public int Id { get; set; }
}
