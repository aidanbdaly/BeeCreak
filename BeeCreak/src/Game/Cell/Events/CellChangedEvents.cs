public class CellChangedEvent
{
    public CellChangedEvent(CellState cellState, string cellId)
    {
        CellState = cellState;
        CellId = cellId;
    }

    public CellState CellState { get; }

    public string CellId { get; }
}