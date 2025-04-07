using System.Collections.Generic;

public class GameState
{
    public string ActiveCell { get; set; }

    public Dictionary<string, CellState> Cells { get; set; }
} 