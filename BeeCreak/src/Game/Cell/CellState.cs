public record CellState
{
    public List<EntityState> EntityStates { get; set; } = [];

    public Grid<TileState> TileStates { get; set; }
}