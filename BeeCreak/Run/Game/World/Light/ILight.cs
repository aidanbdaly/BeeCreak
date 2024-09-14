using BeeCreak.Run.GameObjects;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World.Light;
public interface ILight : IDynamicDrawable
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public int Period { get; set; }
}
