using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Generation;

public class Tile
{
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public bool IsSolid { get; set; }

    public Tile() { }

    public Tile(Texture2D texture, Vector2 position, bool isSolid = false)
    {
        Texture = texture;
        Position = position;
        IsSolid = isSolid;
    }
}
