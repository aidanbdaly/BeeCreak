using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Light;

public class LightDTO
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public int Period { get; set; }
    public float Scale { get; set; } = 1f;
    public float MaxScale { get; set; } = 1.5f;

    public Light FromDTO(IToolCollection tools)
    {
        return new Light(tools, Position, Radius, MaxScale, Period) { Scale = Scale };
    }
}
