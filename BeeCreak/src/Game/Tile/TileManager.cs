using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

public class TileManager
{
    public event EventHandler<EventArgs> OnStateImported;

    public event EventHandler<TileChangedEvent> TileVariantChanged;

    private readonly TileFactory tileFactory;

    public TileManager(CellManager cellManager, TileFactory tileFactory)
    {
        this.tileFactory = tileFactory;

        cellManager.StateImported += (sender, args) => Import(cellManager.Cell.State.TileMap);
    }

    private Grid<TileState> TileState { get; set; }


    public Tile this[int x, int y]
    {
        get => tileFactory.CreateTile(TileState[x, y]);
        set => TileState[x, y] = value.State;
    }

    public Tile this[Point point]
    {
        get => tileFactory.CreateTile(TileState[point.X, point.Y]);
        set => TileState[point.X, point.Y] = value.State;
    }

    public int Width => TileState.Width;

    public int Height => TileState.Height;

    public IEnumerable<(int x, int y, Tile tile)> Enumerate()
    {
        foreach (var (x, y, state) in TileState.Enumerate())
        {
            yield return (x, y, tileFactory.CreateTile(state));
        }
    }

    private void Import(Grid<TileState> tileState)
    {
        TileState = tileState;
    }
}