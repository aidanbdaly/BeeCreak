using BeeCreak.Shared.Services;

public class CellFactory
{
    private readonly AssetManager assetManager;

    public CellFactory(AssetManager assetManager)
    {
        this.assetManager = assetManager;
    }

    public Cell CreateCell(CellState cellState, string id)
    {
        var attributes = assetManager.Load<CellAttributes>($"Cell/{id}");

        return new Cell
        {
            State = cellState,
            Attributes = attributes
        };
    }
}