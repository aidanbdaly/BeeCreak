using BeeCreak.Game;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene.Light;

public interface ILight : IDynamicRenderable
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public int Period { get; set; }
    public LightDTO ToDTO();
}
