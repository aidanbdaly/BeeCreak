using System.Collections.Generic;

public class GameStateDTO
{
    public string ActiveCell { get; set; }

    public Dictionary<string, CellStateDTO> Cells { get; set; }
} 