using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Presentation.Primitives;
using BeeCreak.src.Models;

namespace BeeCreak
{
    public class TileComponent : SpriteComponent
    {
        private readonly Tile tile;

        public TileComponent(Tile tile, AssetHandle<SpriteSheet> spriteSheetHandle) : base(spriteSheetHandle)
        {
            this.tile = tile;

            tile.OnStateChanged += HandleTileStateChanged;
        }

        public override void Dispose()
        {
            tile.OnStateChanged -= HandleTileStateChanged;
            base.Dispose();
        }

        public void HandleTileStateChanged(object sender, EventArgs e)
        {
            SetSprite($"{tile.State.ContentId}_{tile.State.Variant}");
        }
    }
}