using System.Collections.Generic;

namespace BeeCreak.Play.State
{
    public record GameState(
        string ActiveCellId,
        Dictionary<string, CellState> Cells
    );
}
