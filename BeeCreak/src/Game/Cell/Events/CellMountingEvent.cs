using System;

public class CellMountingEvent : EventArgs
{
    public CellState NewCell { get; }

    public CellMountingEvent(CellState newCell)
    {
        NewCell = newCell;
    }
}