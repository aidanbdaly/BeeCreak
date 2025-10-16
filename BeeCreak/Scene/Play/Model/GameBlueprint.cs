using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BeeCreak
{
    /* GameBlueprint represents the initial state of the game.
     * It contains the active cell ID and a list of cells.
     * The DefaultState method returns a GameState with the active cell ID and an empty CellState dictionary.
     * It does not contain CellBlueprints, as we lazy load them.
     */
    public class GameBlueprint
    {
        public string ActiveCellId { get; set; } = string.Empty;

        public List<string> Cells { get; set; } = [];

        public GameState DefaultState => new()
        {
            ActiveCellId = ActiveCellId,
            CellState = new ConcurrentDictionary<string, CellState>()
        };
    }
}
