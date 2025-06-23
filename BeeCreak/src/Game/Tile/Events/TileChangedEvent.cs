using BeeCreak.src.Models;

public class TileChangedEvent : EventArgs
{
    public Tile Tile { get; set; }

    public TileChangedEvent(Tile tile)
    {
        Tile = tile;
    }
}