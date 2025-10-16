using System.Collections.Concurrent;

namespace BeeCreak
{
    public class SaveState
    {
        public ConcurrentDictionary<string, CellState> CellState { get; set; } = new();

        public string ActiveCellId { get; set; } = string.Empty;

        public CellState ActiveCell => CellState.GetOrAdd(ActiveCellId, (_) => new CellState { ContentId = ActiveCellId });
    }
}
