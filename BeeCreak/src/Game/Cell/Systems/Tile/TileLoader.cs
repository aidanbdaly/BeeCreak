public class TileLoader
{
    private readonly TileManager tileManager;

    public TileLoader(CellMounter cellMounter, TileManager tileManager)
    {
        this.tileManager = tileManager;

        cellMounter.CellUnmounting += HandleCellUnmounting;
        cellMounter.CellMounting += HandleCellMounting;
    }

    protected virtual void HandleCellUnmounting(object? sender, CellUnmountingEvent e)
    {
        e.CurrentCell.TileStates = tileManager.Tiles;
    }

    protected virtual void HandleCellMounting(object? sender, CellMountingEvent e)
    {
        tileManager.Tiles = e.NewCell.TileStates;
    }
}
