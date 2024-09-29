using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Tile;

public abstract class TileDTO
{
    public TileType Type;
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; set; }

    public abstract Tile FromDTO(IToolCollection tools);
}
