
using BeeCreak.src.Models;

namespace BeeCreak
{
    public class Cell
    {
        public string ContentId { get; init; }
        
        public List<Tile> TileMap { get; set; }
    
        public List<Entity> EntityList { get; set; } = [];
    }
}
