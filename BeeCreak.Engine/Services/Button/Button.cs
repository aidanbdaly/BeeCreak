namespace BeeCreak.Shared.UI;

public class Button
{
    public Button(Action onClick, string text = "")
    {
        Text = text;
        OnClick = onClick;
    }

    public Action? OnClick { get; set; }

    public string Text { get; set; } = string.Empty;
}
