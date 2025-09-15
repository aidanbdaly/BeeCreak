using BeeCreak.src.Models;

namespace BeeCreak
{
    
    public class TileChangedEvent : EventArgs
    {
        public Tile Tile { get; set; }
    
        public TileChangedEvent(Tile tile)
        {
            Tile = tile;
        }
    }
}
