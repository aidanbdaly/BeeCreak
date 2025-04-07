using System.Linq;

public class CellFactory
{
    private readonly EntityFactory entityFactory;

    private readonly TileFactory tileFactory;

    public CellFactory(EntityFactory entityFactory, TileFactory tileFactory)
    {
        this.entityFactory = entityFactory;
        this.tileFactory = tileFactory;
    }

    public CellState fromDTO(CellStateDTO cellState)
    {
        var tileStates = new Tile[cellState.TileStates.GetLength(0), cellState.TileStates.GetLength(1)];

        for (int i = 0; i < cellState.TileStates.GetLength(0); i++)
        {
            for (int j = 0; j < cellState.TileStates.GetLength(1); j++)
            {
                tileStates[i, j] = tileFactory.fromDTO(cellState.TileStates[i, j]);
            }
        }

        return new CellState()
        {
            TileStates = tileStates,
            EntityStates = cellState.EntityStates.Select(entityFactory.fromDTO).ToList()
        };
    }

    public CellStateDTO toDTO(CellState cellState)
    {
        var tileStates = new TileDTO[cellState.TileStates.GetLength(0), cellState.TileStates.GetLength(1)];

        for (int i = 0; i < cellState.TileStates.GetLength(0); i++)
        {
            for (int j = 0; j < cellState.TileStates.GetLength(1); j++)
            {
                tileStates[i, j] = tileFactory.toDTO(cellState.TileStates[i, j]);
            }
        }

        return new CellStateDTO()
        {
            TileStates = tileStates,
            EntityStates = cellState.EntityStates.Select(entityFactory.toDTO).ToList()
        };
    }
}