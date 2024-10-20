using BeeCreak.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Generation;
public class RouterBit
{
    public Vector2 Position { get; set; }
    public Direction Direction { get; set; }
    public RouterBit(Vector2 position, Direction direction)
    {
        Position = position;
        Direction = direction;
    }
}
