using BeeCreak.Engine.Asset;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public class TileComponentFactory
    {
        private readonly AssetManager assetManager;

        public TileComponentFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public TileComponent CreateTileComponent(Tile tile)
        {
            return new TileComponent(tile, assetManager.Acquire<SpriteSheet>("Spritesheet/tiles"));
        }
    }
}