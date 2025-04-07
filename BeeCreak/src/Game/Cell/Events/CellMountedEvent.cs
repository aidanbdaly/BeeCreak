using System;

public class CellMountedEvent : EventArgs
{
    public CellState Cell { get; }

    public CellMountedEvent(CellState cell)
    {
        Cell = cell;
    }
}