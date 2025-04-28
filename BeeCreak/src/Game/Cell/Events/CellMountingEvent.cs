using System;

public class CellMountingEvent : EventArgs
{
    public CellState NewCell { get; }

    public CellState CurrentCell { get; }

    public CellMountingEvent(CellState newCell, CellState currentCell)
    {
        CurrentCell = currentCell;
        NewCell = newCell;
    }
}