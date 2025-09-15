using System.Collections.Concurrent;

namespace BeeCreak
{
    
    public class GameState
    {
        public ConcurrentDictionary<string, CellState> CellState { get; set; } = new();
    
        public string ActiveCellId { get; set; } = string.Empty;
    
        public CellSkeleton ActiveCell => new(ActiveCellId, CellState.GetOrAdd(ActiveCellId, (_) => new CellState()));
    }
}
