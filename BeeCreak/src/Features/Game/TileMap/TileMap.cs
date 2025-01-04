using System;
using BeeCreak.Config;
using BeeCreak.Features.Game.Tile;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Tile;

public class TileMap : ITileMap
{
    private readonly ISprite sprite;

    private readonly ITileAtlas tileAtlas;

    private readonly ITileVariantCalculator tileVariator;

    public TileMap(ISprite sprite, ITileAtlas tileAtlas, ITileVariantCalculator tileVariator)
    {
        this.sprite = sprite;
        this.tileAtlas = tileAtlas;
        this.tileVariator = tileVariator;
    }

    public ITile[,] Tiles { get; set; }

    private RenderTarget2D Target { get; set; }

    public void SetTiles(ITile[,] tiles)
    {
        Tiles = tiles;

        tileVariator.SetTiles(Tiles);
    }

    public ITile GetTile(int x, int y)
    {
        return Tiles[x, y];
    }

    public void Bake()
    {
        var size = Tiles.GetLength(0);

        var sizeInPixels = Globals.TileSize * size;

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

        for (var x = 0; x < Tiles.GetLength(0); x++)
        {
            for (var y = 0; y < Tiles.GetLength(1); y++)
            {
                var tile = Tiles[x, y];

                tileAtlas.DrawTile(tile);
            }
        }

        sprite.Batch.End();
    }

    public void RecalculateTileVariants()
    {
        tileVariator.RecalculateTileVariants();
    }

    public void Draw()
    {
        if (Target == null)
        {
            throw new InvalidOperationException("Cannot draw unbaked tile map.");
        }

        sprite.Batch.Draw(Target, Vector2.Zero, Color.White);
    }
}
