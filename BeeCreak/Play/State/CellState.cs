using System.Collections.Generic;
using BeeCreak;

namespace BeeCreak.Play.State
{
    public record CellState(
        string Id,
        List<TileRecord> Tiles,
        List<EntityRecord> Entities
    );
}
