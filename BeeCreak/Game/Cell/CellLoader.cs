using BeeCreak.Scene.Main;
public class CellLoader
{
    private readonly GameContext context;

    private readonly TileManager tileManager;

    private readonly EntityManager entityManager;

    public CellLoader(GameContext context, TileManager tileManager, EntityManager entityManager)
    {
        this.context = context;

        this.tileManager = tileManager;
        this.entityManager = entityManager;
    }

    public void MountCell(string cellId)
    {
        if (context.Instance.ActiveCell == cellId)
        {
            return;
        }

        HandleCellUnmounting();

        context.Instance.ActiveCell = cellId;

        HandleCellMounting();

    }

    private void HandleCellUnmounting()
    {
        var currentCell = context.GetActiveCell();

        if (currentCell != null)
        {
            currentCell.TileStates = tileManager.Tiles;
            currentCell.EntityStates = entityManager.Export();
        }
    }

    private void HandleCellMounting()
    {
        var currentCell = context.GetActiveCell();

        if (currentCell != null)
        {
            tileManager.Tiles = currentCell.TileStates;
            entityManager.Import(currentCell.EntityStates);
        }
    }
}