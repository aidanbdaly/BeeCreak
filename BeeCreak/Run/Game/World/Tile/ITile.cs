using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Tile;
public interface ITile
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; set; }
    public bool Opaque { get; set; }
}
