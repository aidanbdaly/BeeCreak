using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Shared.UI;

public class Button
{
    public EventHandler<EventArgs>? OnClick { get; set; }

    public EventHandler<EventArgs>? OnHover { get; set; }

    public ButtonStatus Status { get; set; } = ButtonStatus.Normal;

    public Vector2 Position { get; set; }

    public Rectangle Bounds { get; set; }

    public string Text { get; set; } = string.Empty;

    public void DispatchOnClickEvent()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    public void DispatchOnHoverEvent()
    {
        OnHover?.Invoke(this, EventArgs.Empty);
    }
}
