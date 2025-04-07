using BeeCreak.Scene.Main;

public class EntityLoader
{
    public EntityLoader(CellMounter cellMounter)
    {
        cellMounter.CellUnmounting += HandleCellUnmounting;
        cellMounter.CellMounting += HandleCellMounting;
    }

    protected virtual void HandleCellUnmounting(object sender, CellUnmountingEvent e)
    {
        e.CurrentCell.EntityStates = EntityManager.ExportState();
        
        EntityManager.Clear();
    }

    protected virtual void HandleCellMounting(object sender, CellMountingEvent e)
    {
        EntityManager.ImportState(e.NewCell.EntityStates);
    }
}