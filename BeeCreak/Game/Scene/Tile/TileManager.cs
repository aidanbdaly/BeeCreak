namespace BeeCreak.Game.Scene.Tile
{
    using global::BeeCreak.Config;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TileManager
    {
        private readonly ISprite sprite;

        public TileManager(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public ITileMap TileMap { get; set; }

        private RenderTarget2D Target { get; set; }

        public void SetTileMap(ITileMap tileMap)
        {
            var tiles = tileMap.Tiles;

            var sizeInPixels = Globals.TileSize * tiles.GetLength(0);

            Target = new RenderTarget2D(
              sprite.GraphicsDevice,
              sizeInPixels,
              sizeInPixels,
              false,
              SurfaceFormat.Color,
              DepthFormat.None,
              0,
              RenderTargetUsage.PreserveContents);

            sprite.GraphicsDevice.SetRenderTarget(Target);

            sprite.Batch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend);

            for (var x = 0; x < tiles.GetLength(0); x++)
            {
                for (var y = 0; y < tiles.GetLength(1); y++)
                {
                    var tile = tiles[x, y];

                    sprite.Batch.Draw(
                        tile.Texture,
                        new Vector2(x * Globals.TileSize, y * Globals.TileSize),
                        Color.White);
                }
            }

            sprite.Batch.End();
        }

        public void DrawTile(int x, int y)
        {
            var tile = TileMap.Tiles[x, y];

            sprite.GraphicsDevice.SetRenderTarget(Target);

            sprite.Batch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend);

            sprite.Batch.Draw(
                tile.Texture,
                new Vector2(x * Globals.TileSize, y * Globals.TileSize),
                Color.White);

            sprite.Batch.End();
        }

        public void Draw()
        {
            sprite.Batch.Draw(Target, Vector2.Zero, Color.White);
        }
    }
}
