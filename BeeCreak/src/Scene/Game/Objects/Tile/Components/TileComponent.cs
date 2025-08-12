using BeeCreak.src.Models;

namespace BeeCreak
{
    public class TileComponent : SpriteComponent, IDisposable
    {
        private readonly Tile tile;

        public TileComponent(Tile tile, SpriteSheet spriteSheet) : base(spriteSheet)
        {
            this.tile = tile;

            tile.OnStateChanged += HandleTileStateChanged;
        }

        public void Dispose()
        {
            tile.OnStateChanged -= HandleTileStateChanged;
            GC.SuppressFinalize(this);
        }

        public void HandleTileStateChanged(object sender, EventArgs e)
        {
            SetSprite($"{tile.State.ContentId}_{tile.State.Variant}");
        }
    }
}