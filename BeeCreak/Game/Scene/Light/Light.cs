using System;
using BeeCreak.Generation;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Scene.Light;

public class Light : ILight
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public int Period { get; set; }
    public float Scale { get; set; } = 1f;
    public float MaxScale { get; set; } = 1.5f;
    private Texture2D LightMap { get; set; }
    private IToolCollection Tools { get; set; }

    public Light(IToolCollection tools, Vector2 position, int radius, float maxScale, int period)
    {
        Tools = tools;
        Position = position;
        Radius = radius;

        MaxScale = maxScale;
        Period = period;

        LightMap = new Texture2D(tools.Static.GraphicsDevice, Radius * 2 + 1, Radius * 2 + 1);

        GetLightTarget();
    }

    public Light(IToolCollection tools, Vector2 position, int radius)
    {
        Tools = tools;
        Position = position;
        Radius = radius;

        MaxScale = 1f;
        Period = 0;

        LightMap = new Texture2D(tools.Static.GraphicsDevice, Radius * 2, Radius * 2);

        GetLightTarget();
    }

    public LightDTO ToDTO()
    {
        return new LightDTO
        {
            Position = Position,
            Radius = Radius,
            Period = Period,
            Scale = Scale,
            MaxScale = MaxScale
        };
    }

    public void GetLightTarget()
    {
        var diameter = Radius * 2 + 1; // since the origin (0, 0) is also a tile

        var colorData = new Color[diameter * diameter];

        foreach (var (x, y) in Shape.Circle(Radius).Coordinates)
        {
            float distanceToLight = Vector2.Distance(new Vector2(x, y), Vector2.Zero);

            float intensity = MathHelper.Clamp(1 - distanceToLight / Radius, 0f, 1f);

            byte intensityByte = (byte)(intensity * 255);

            int indexX = x + Radius; // Shift x to ensure it's within bounds
            int indexY = y + Radius; // Shift y to ensure it's within bounds
            int arrayIndex = indexY * diameter + indexX;

            colorData[arrayIndex] = new Color(
                intensityByte,
                intensityByte,
                intensityByte,
                (byte)255
            );
        }

        LightMap.SetData(colorData);
    }

    public void Update(GameTime gameTime)
    {
        var newScale =
            1f
            + (MaxScale - 1f)
                * 0.5f
                * (
                    1f + (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI / Period)
                );

        if (Math.Abs(newScale - Scale) > 0.001f)
        {
            Scale = newScale;
        }
    }

    public void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(
            LightMap,
            new Vector2(
                Position.X * Tools.Static.TILE_SIZE + 16,
                Position.Y * Tools.Static.TILE_SIZE + 16
            ),
            null,
            Color.White,
            0f,
            new Vector2(LightMap.Width / 2f, LightMap.Height / 2f),
            Tools.Static.TILE_SIZE * Scale,
            SpriteEffects.None,
            0f
        );
    }
}
