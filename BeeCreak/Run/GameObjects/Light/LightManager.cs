using System.Collections.Generic;
using BeeCreak.Run.GameObjects.Delegates;
using BeeCreak.Run.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects;

public class LightManager
{
    public List<LightSource> LightSources { get; set; }
    private Texture2D LightMap { get; set; }
    public RenderTarget2D LightTarget { get; set; }
    private IsOpaqueDelegate IsOpaque { get; set; }
    private int Size { get; set; }
    private IToolCollection Tools { get; set; }

    public LightManager(
        IToolCollection tools,
        IsOpaqueDelegate isOpaque,
        int size,
        int sizeInPixels
    )
    {
        LightSources = new()
        {
            new() { Position = new Vector2(size / 2, size / 2), Radius = 5 }, // Sun
        };

        IsOpaque = isOpaque;

        Tools = tools;

        LightMap = new Texture2D(tools.Static.GraphicsDevice, size, size);

        LightTarget = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            sizeInPixels,
            sizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents
        );

        Size = size;

        CalculateLightTarget();
    }

    public void UpdateLightAtTile(int x, int y)
    {
        Color[] colorData = new Color[Size * Size];

        LightMap.GetData(colorData);

        foreach (var light in LightSources)
        {
            float distanceToLight = Vector2.Distance(new Vector2(x, y), light.Position);

            float intensity = MathHelper.Clamp(1 - distanceToLight / light.Radius, 0f, 1f);

            byte intensityByte = (byte)(intensity * 255);

            if (!IsOpaque(x, y))
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

    private void CalculateLightTarget()
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

                if (!IsOpaque(X, Y))
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

        Tools.Static.GraphicsDevice.SetRenderTarget(null);

        Tools.Static.GraphicsDevice.SetRenderTarget(LightTarget);

        Tools.Static.GraphicsDevice.Clear(Color.Transparent);

        Tools.Static.Sprite.Batch.Begin(blendState: BlendState.AlphaBlend);

        Tools.Static.Sprite.Batch.Draw(
            LightMap,
            Vector2.Zero,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Tools.Static.TILE_SIZE,
            SpriteEffects.None,
            0f
        );

        Tools.Static.Sprite.Batch.End();
    }
}
