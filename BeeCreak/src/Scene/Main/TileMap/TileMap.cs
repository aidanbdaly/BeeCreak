using System;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Main;

// this is both a tilemap and a tilemap controllers

public class TileMap : ITileMap
{
    private readonly ISpriteController spriteController;

    private readonly ITileVariantCalculator tileVariator;

    public TileMap(ISpriteController spriteController, ITileVariantCalculator tileVariator)
    {
        this.spriteController = spriteController;
        this.tileVariator = tileVariator;
    }

    public ITile[,] Tiles { get; set; }

    private RenderTarget2D Target { get; set; }

    public ITile GetTile(int x, int y)
    {
        return Tiles[x, y];
    }

    public void Bake()
    {
        var size = Tiles.GetLength(0);

        var sizeInPixels = Globals.TILE_SIZE * size;

        Target = new RenderTarget2D(
          spriteController.GraphicsDevice,
          sizeInPixels,
          sizeInPixels,
          false,
          SurfaceFormat.Color,
          DepthFormat.None,
          0,
          RenderTargetUsage.PreserveContents);

        spriteController.GraphicsDevice.SetRenderTarget(Target);

        spriteController.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend);

        for (var x = 0; x < Tiles.GetLength(0); x++)
        {
            for (var y = 0; y < Tiles.GetLength(1); y++)
            {
                var tile = Tiles[x, y];
                
                tile.Draw();
            }
        }

        spriteController.Batch.End();
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

        spriteController.Batch.Draw(Target, Vector2.Zero, Color.White);
    }
}
