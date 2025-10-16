namespace BeeCreak
{
    public class CellState
    {
        public string ContentId { get; init; }

        public List<TileState> TileMap { get; set; }

        public List<EntityState> EntityList { get; set; } = [];
    }
}
