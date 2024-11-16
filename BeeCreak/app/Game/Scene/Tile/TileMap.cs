using BeeCreak.Config;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Tile
{
    public class TileMap : ITileMap
    {
        private readonly ISprite sprite;

        public TileMap(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public ITile[,] Tiles { get; set; }

        private RenderTarget2D Target { get; set; }

        public void SetTiles(ITile[,] tiles)
        {
            Tiles = tiles;

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

        public ITile GetTile(int x, int y)
        {
            return Tiles[x, y];
        }

        public void Draw()
        {
            sprite.Batch.Draw(Target, Vector2.Zero, Color.White);
        }
    }
}