using BeeCreak.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Tile;

public class TileManager
{
    public ITile[,] Tiles { get; set; }
    private RenderTarget2D Target { get; set; }
    private IToolCollection Tools { get; set; }

    public TileManager(IToolCollection tools, ITile[,] tiles, int size)
    {
        Tools = tools;
        Tiles = tiles;

        var sizeInPixels = size * Tools.Static.TILE_SIZE;

        Target = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            sizeInPixels,
            sizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents
        );

        Tools.Static.GraphicsDevice.SetRenderTarget(Target);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        foreach (var tile in Tiles)
        {
            tile.Draw();
        }

        Tools.Static.Sprite.Batch.End();
    }

    public void DrawTile(int x, int y)
    {
        var tile = Tiles[x, y];

        var sourceRectangle = new Rectangle
        {
            X = (int)tile.Position.X,
            Y = (int)tile.Position.Y,
            Width = Tools.Static.TILE_SIZE,
            Height = Tools.Static.TILE_SIZE
        };

        Tools.Static.GraphicsDevice.SetRenderTarget(Target);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        tile.Draw();

        Tools.Static.Sprite.Batch.End();
    }

    public void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(Target, Vector2.Zero, Color.White);
    }
}
