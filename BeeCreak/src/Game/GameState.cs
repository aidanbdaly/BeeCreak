public class GameState
{
    public event EventHandler<CellChangedEvent> OnCellChanged;

    public GameState(string activeCellId)
    {
        ActiveCell = activeCellId;
        Cells = [];
    }

    private string activeCell;

    public string ActiveCell
    {
        get
        {
            return activeCell;
        }
        set
        {
            if (value != activeCell)
            {
                activeCell = value;
                OnCellChanged?.Invoke(this, new CellChangedEvent(Cells[activeCell], activeCell));
            }
        }
    }

    public Dictionary<string, CellState> Cells { get; }
}