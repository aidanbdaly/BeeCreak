using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Tile;

public class TileManager
{
    public ICell Cell { get; set; }
    public RenderTarget2D Target { get; set; }
    private IToolCollection Tools { get; set; }

    public TileManager(IToolCollection tools, ICell cell)
    {
        Tools = tools;
        Cell = cell;

        var sizeInPixels = cell.Size * Tools.Static.TILE_SIZE;

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

        DrawTarget();
    }

    public void DrawTile(int x, int y)
    {
        var tile = Cell.Map[x, y];

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

        Tools.Static.Sprite.Batch.Draw(tile.Texture, sourceRectangle, Color.White);

        Tools.Static.Sprite.Batch.End();
    }

    private void DrawTarget()
    {
        Tools.Static.GraphicsDevice.SetRenderTarget(Target);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        foreach (var tile in Cell.Map)
        {
            Tools.Static.Sprite.Batch.Draw(tile.Texture, tile.Position, Color.White);
        }

        Tools.Static.Sprite.Batch.End();
    }

    public void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(Target, Vector2.Zero, Color.White);
    }
}
