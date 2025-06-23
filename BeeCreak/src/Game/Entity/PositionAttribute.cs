using Microsoft.Xna.Framework;

public class PositionAttribute
{
    public Vector2 Position { get; set; }

    public PositionAttribute(Vector2 position)
    {
        Position = position;
    }
}