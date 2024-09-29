using BeeCreak.Run.Game;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Light;

public interface ILight : IDynamicDrawable
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public int Period { get; set; }
    public LightDTO ToDTO();
}
