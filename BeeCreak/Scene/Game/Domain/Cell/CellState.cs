
namespace BeeCreak
{
    public class CellState
    {
        public string ContentId { get; init; }
        
        public Grid<TileState> TileState { get; set; }
    
        public Dictionary<string, EntityState> EntityState { get; set; } = [];
    }
}
