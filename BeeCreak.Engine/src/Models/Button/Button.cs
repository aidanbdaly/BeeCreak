using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.UI;

public class Button
{
    public ButtonComponentState State { get; set; }

    public Vector2 Position { get; set; }

    public Rectangle Bounds { get; set; }

    public string Action { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}
