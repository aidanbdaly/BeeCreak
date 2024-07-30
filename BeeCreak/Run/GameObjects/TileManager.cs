using System.Collections.Generic;
using BeeCreak.Run.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects;

public class TileManager
{
    public Tile[,] TileSet { get; set; } = default!;
    public Texture2D LightMap { get; set; } = default!;
    public RenderTarget2D SceneTarget { get; set; } = default!;
    private int Size { get; set; }
    private int SizeInPixels => Size * Tools.Static.TILE_SIZE;
    private List<LightSource> LightSources { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public TileManager(IToolCollection tools, int size, int seed, List<LightSource> lightSources)
    {
        Tools = tools;

        var shapeRouter = new ShapeRouter(tools, seed, size);

        Size = size;

        TileSet = shapeRouter.Route();

        LightMap = new Texture2D(tools.Static.GraphicsDevice, size, size);

        SceneTarget = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            SizeInPixels,
            SizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents
        );

        LightSources = lightSources;

        CalculateLightMap();
        DrawTiles();
    }

    public bool IsInWorld(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Size && y < Size;
    }

    public void UpdateLightMapPoint(int x, int y)
    {
        Color[] colorData = new Color[Size * Size];

        LightMap.GetData(colorData);

        foreach (var light in LightSources)
        {
            float distanceToLight = Vector2.Distance(new Vector2(x, y), light.Position);

            float intensity = MathHelper.Clamp(1 - distanceToLight / light.Radius, 0f, 1f);

            byte intensityByte = (byte)(intensity * 255);

            if (IsInWorld(x, y) && !TileSet[x, y].IsSolid)
            {
                colorData[y * Size + x] = new Color(
                    intensityByte,
                    intensityByte,
                    intensityByte,
                    (byte)255
                );
            }
        }

        LightMap.SetData(colorData);
    }

    public void RedrawTile(int x, int y)
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

        Tools.Static.GraphicsDevice.SetRenderTarget(null);
    }

    private void CalculateLightMap()
    {
        Color[] colorData = new Color[Size * Size];

        LightMap.GetData(colorData);

        foreach (var light in LightSources)
        {
            foreach (var (x, y) in Shape.Circle(light.Radius).Coordinates)
            {
                int X = (int)light.Position.X + x;
                int Y = (int)light.Position.Y + y;

                //float prevIntensity = colorData[X * Size + Y].A / 255f;

                float distanceToLight = Vector2.Distance(new Vector2(X, Y), light.Position);

                float intensity = MathHelper.Clamp(1 - distanceToLight / light.Radius, 0f, 1f);

                byte intensityByte = (byte)(intensity * 255);

                if (IsInWorld(X, Y) && !TileSet[X, Y].IsSolid)
                {
                    colorData[Y * Size + X] = new Color(
                        intensityByte,
                        intensityByte,
                        intensityByte,
                        (byte)255
                    );
                }
            }
        }

        LightMap.SetData(colorData);
    }

    private void DrawTiles()
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
