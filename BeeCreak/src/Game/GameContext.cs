public class GameContext
{
    public string SaveId { get; set; }

    public GameState Instance { get; set; }

    public CellState GetActiveCell() {
        return Instance.Cells[Instance.ActiveCell];
    }
}