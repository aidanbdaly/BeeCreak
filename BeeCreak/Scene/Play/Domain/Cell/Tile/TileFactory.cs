using BeeCreak.Engine.Assets;
using BeeCreak.src.Models;

namespace BeeCreak
{   
    public class TileFactory
    {
        private readonly AssetHandle<TileCatalogue> tileCatalogue;
    
        public TileFactory(AssetManager assetManager)
        {
            tileCatalogue = assetManager.Acquire<TileCatalogue>(ContentPaths.TileCatalogue);
        }
    
        public Tile CreateTile(TileState state)
        {
            if (tileCatalogue.Asset.TryGetValue(state.ContentId, out var tileData))
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
