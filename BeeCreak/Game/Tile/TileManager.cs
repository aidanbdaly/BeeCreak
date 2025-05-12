public class TileManager
{
    public event EventHandler<EventArgs>? StateImported;

    public event EventHandler<TileChangedEvent>? TileTypeChanged;

    public event EventHandler<TileChangedEvent>? TileVariantChanged;

    private Tile[,]? tiles { get; set; }

    public Tile[,] Tiles
    {
        get => tiles ?? throw new InvalidOperationException("Get on tiles before set");
        set
        {
            tiles = value;
            StateImported?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UpdateType(int x, int y, string type)
    {
        if (Tiles[x, y] == null)
        {
            return;
        }

        var tile = Tiles[x, y].WithType(type);

        Tiles[x, y] = tile;

        TileTypeChanged?.Invoke(this, new TileChangedEvent(tile));
    }

    public void UpdateVariant(int x, int y, string variant)
    {
        if (Tiles[x, y] == null)
        {
            return;
        }

        var tile = Tiles[x, y].WithVariant(variant);

        Tiles[x, y] = tile;

        TileVariantChanged?.Invoke(this, new TileChangedEvent(tile));
    }
}