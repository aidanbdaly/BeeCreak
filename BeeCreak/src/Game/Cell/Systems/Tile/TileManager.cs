using System;

public class TileManager
{
    public event EventHandler<EventArgs> TileChanged;

    private Tile[,] _tiles;

    public Tile[,] Tiles
    {
        get => _tiles;
        set
        {
            _tiles = value;
            TileChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void UpdateTile(int x, int y, Tile tile)
    {
        Tiles[x, y] = tile;

        TileChanged?.Invoke(this, EventArgs.Empty);
    }
}