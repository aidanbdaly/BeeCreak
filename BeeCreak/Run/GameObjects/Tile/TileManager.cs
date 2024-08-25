using System.Collections.Generic;
using BeeCreak.Run.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects;

public class TileManager
{
    public int Size { get; set; }
    public Tile[,] TileSet { get; set; }
    public RenderTarget2D SceneTarget { get; set; }
    private IToolCollection Tools { get; set; }
    public RenderTarget2D EntityTarget { get; set; }

    public TileManager(IToolCollection tools, int size, int sizeInPixels, int seed)
    {
        Tools = tools;
        Size = size;

        var shapeRouter = new ShapeRouter(tools, seed, size);

        TileSet = shapeRouter.Route();

        SceneTarget = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            sizeInPixels,
            sizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents
        );

        CalculateSceneTarget();
    }

    public void DrawTile(int x, int y)
    {
        var tile = TileSet[x, y];

        var sourceRectangle = new Rectangle
        {
            X = (int)tile.Position.X,
            Y = (int)tile.Position.Y,
            Width = Tools.Static.TILE_SIZE,
            Height = Tools.Static.TILE_SIZE
        };

        Tools.Static.GraphicsDevice.SetRenderTarget(SceneTarget);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        Tools.Static.Sprite.Batch.Draw(tile.Texture, sourceRectangle, Color.White);

        Tools.Static.Sprite.Batch.End();
    }

    private void CalculateSceneTarget()
    {
        Tools.Static.GraphicsDevice.SetRenderTarget(SceneTarget);
        Tools.Static.GraphicsDevice.Clear(Color.Black);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                var tile = TileSet[x, y];
                Tools.Static.Sprite.Batch.Draw(tile.Texture, tile.Position, Color.White);
            }
        }

        Tools.Static.Sprite.Batch.End();
    }
}
