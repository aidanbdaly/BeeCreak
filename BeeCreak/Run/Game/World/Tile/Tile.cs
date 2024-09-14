using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Tile;

public class Tile : ITile
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; set; }
    public bool Opaque { get; set; }

    public Tile() { }

    public Tile(
        Texture2D texture,
        Vector2 position,
        Rectangle bounds = default,
        bool opaque = false
    )
    {
        Texture = texture;
        Position = position;
        Bounds = bounds;
        Opaque = opaque;
    }
}
