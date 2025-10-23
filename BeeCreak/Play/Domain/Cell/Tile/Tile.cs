using Microsoft.Xna.Framework;

namespace BeeCreak.src.Models;

public class Tile : ITile
{
    public Tile(TileState state, TileAttributes attributes)
    {
        State = state;
        Attributes = attributes;
    }

    public TileState State { get; }

    public TileAttributes Attributes { get; }

    public bool HasCollision => Attributes.HitBox != Rectangle.Empty;
}