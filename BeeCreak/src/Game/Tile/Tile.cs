namespace BeeCreak.src.Models;

public class Tile : ITile
{
    public Tile(TileState state, TileAttributes attributes)
    {
        State = state;
        Attributes = attributes;
    }

    public string Type { get; }
    
    public TileState State { get; }

    public TileAttributes Attributes { get; }

    public bool HasCollision => Attributes.HitBox is not null;

    public bool IsVariable => Attributes.IsVariable;
}