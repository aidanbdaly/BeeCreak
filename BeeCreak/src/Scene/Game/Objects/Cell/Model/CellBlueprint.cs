namespace BeeCreak.src.Models;

public record CellBlueprint
(
    IReadOnlyList<EntityState> EntityState,
    ReadOnlyGrid<TileState> TileState
);