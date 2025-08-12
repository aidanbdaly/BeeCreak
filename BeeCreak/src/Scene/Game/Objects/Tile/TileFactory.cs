using BeeCreak.Shared.Services;
using BeeCreak.src.Models;

namespace BeeCreak
{
    
    public class TileFactory
    {
        private readonly TileCatalogue tileCatalogue;
    
        public TileFactory(AssetManager assetManager)
        {
            tileCatalogue = assetManager.Load<TileCatalogue>("Tile/Tile");
        }
    
        public Tile CreateTile(TileState state)
        {
            if (tileCatalogue.TryGetValue(state.ContentId, out var tileData))
            {
                return new Tile(state, tileData);
            }
            else
            {
                throw new ArgumentException($"Tile type '{state.ContentId}' not found in catalogue.");
            }
        }
    }
}
