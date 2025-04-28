using System;
using BeeCreak.Scene.Main;
public class CellLoader
{
    private readonly GameContext context;

    private readonly TileManager tileManager;

    private readonly EntityManager entityManager;

    public CellLoader(SaveMounter saveMounter, GameContext context, TileManager tileManager, EntityManager entityManager)
    {
        this.context = context;

        saveMounter.SaveMounted += HandleSaveMounted;

        this.tileManager = tileManager;
        this.entityManager = entityManager;
    }

    public void MountCell(string cellId)
    {
        if (context.SaveId != string.Empty)
        {
            HandleCellUnmounting();
        }

        context.Instance.ActiveCell = cellId;

        HandleCellMounting();
    }

    private void HandleSaveMounted(object? sender, EventArgs e)
    {
        MountCell(context.Instance.ActiveCell);
    }

    protected void HandleCellUnmounting()
    {
        var currentCell = context.GetActiveCell();

        if (currentCell != null)
        {
            currentCell.TileStates = tileManager.Tiles;
            currentCell.EntityStates = entityManager.Export();
        }
    }

    protected void HandleCellMounting()
    {
        var currentCell = context.GetActiveCell();

        if (currentCell != null)
        {
            tileManager.Tiles = currentCell.TileStates;
            entityManager.Import(currentCell.EntityStates);
        }
    }
}