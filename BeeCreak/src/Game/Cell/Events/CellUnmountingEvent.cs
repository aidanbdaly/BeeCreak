using System;

public class CellUnmountingEvent : EventArgs
{
    public CellState CurrentCell { get; }

    public CellUnmountingEvent(CellState currentCell)
    {
        CurrentCell = currentCell;
    }
}