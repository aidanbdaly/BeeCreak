using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class LightDTO
{
    public Vector2 Position { get; set; } = new();

    public int Radius { get; set; }

    public int Period { get; set; }

    public float Scale { get; set; }

    public float MaxScale { get; set; }
}