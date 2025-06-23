using BeeCreak.Shared.Services;
using BeeCreak.src.Models;

public class TileFactory
{
    private TileCatalogue tileCatalogue;

    public void LoadContent(AssetManager assetManager)
    {
        tileCatalogue = assetManager.Load<TileCatalogue>("Catalogue/Tile");
    }

    public Tile CreateTile(TileState state)
    {
        if (tileCatalogue.TryGetValue(state.Type, out var tileData))
        {
            return new Tile(state, tileData);
        }
        else
        {
            throw new ArgumentException($"Tile type '{state.Type}' not found in catalogue.");
        }
    }
}