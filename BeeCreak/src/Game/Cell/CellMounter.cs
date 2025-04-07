using System;
public class CellMounter
{
    private readonly GameContext context;

    public CellMounter(SaveMounter saveMounter, GameContext context)
    {
        this.context = context;

        saveMounter.SaveMounted += HandleSaveMounted;
    }

    public event EventHandler<CellMountedEvent>? CellMounted;

    public event EventHandler<CellUnmountingEvent>? CellUnmounting;

    public event EventHandler<CellMountingEvent>? CellMounting;

    public void MountCell(string cellId)
    {
        if (context.Instance.ActiveCell != null)
        {
            OnCellUnmounting(
                new CellUnmountingEvent(
                    context.GetActiveCell()
                )
            );
        }

        context.Instance.ActiveCell = cellId;

        OnCellMounting(
            new CellMountingEvent(
                context.GetActiveCell()
            )
        );

        OnCellMounted(new CellMountedEvent(
            context.GetActiveCell()
        ));
    }

    protected virtual void OnCellMounted(CellMountedEvent e)
    {
        CellMounted?.Invoke(this, e);
    }

    protected virtual void OnCellUnmounting(CellUnmountingEvent e)
    {
        CellUnmounting?.Invoke(this, e);
    }

    protected virtual void OnCellMounting(CellMountingEvent e)
    {
        CellMounting?.Invoke(this, e);
    }

    private void HandleSaveMounted(object? sender, EventArgs e)
    {
        MountCell(context.Instance.ActiveCell);
    }
}