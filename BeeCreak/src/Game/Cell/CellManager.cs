public class CellManager
{
    public event EventHandler<EventArgs> StateImported;

    private readonly Game game;

    private readonly CellFactory cellFactory;

    public CellManager(Game game, CellFactory cellFactory)
    {
        this.game = game;
        this.cellFactory = cellFactory;

        game.State.OnCellChanged += (sender, args) => Import(args);
    }

    public Cell Cell { get; private set; }

    private void Import(CellChangedEvent args)
    {
        Cell = cellFactory.CreateCell(args.CellState, args.CellId);
    }
}