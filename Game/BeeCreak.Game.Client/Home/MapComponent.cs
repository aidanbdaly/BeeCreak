using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game.Play.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Game.Domain.Map;

public readonly struct Uint2D(uint x, uint y)
{
    public uint X { get; } = x;
    public uint Y { get; } = y;

    public Point ToPoint()
    {
        return new Point((int)X, (int)Y);
    }
}

public sealed class MapComponent(
    App app,
    Uint2D size) : DrawableGameComponent(app)
{
    private readonly Point size = size.ToPoint();

    private Texture2D? texture;

    private RenderTarget2D? frame;

    private Matrix camera;

    private Vector2 offset = Vector2.Zero;

    private float zoom = 1f;

    private MapService MapService
          => app.Services.GetService<MapService>();

    private KeyboardInputService Keyboard
          => app.Services.GetService<KeyboardInputService>();

    protected override void LoadContent()
    {
        GenerateTexture();
        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        texture?.Dispose();
        texture = null;
        base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        static float Clamp(float value, float minValue, float maxValue)
        {
            if (value < minValue) return minValue;
            if (value > maxValue) return maxValue;
            return value;
        }

        if (Keyboard.IsKeyDown(Keys.OemPlus))
        {
            zoom = Clamp(zoom + 0.01f, 1, 5);
        }

        if (Keyboard.IsKeyDown(Keys.OemMinus))
        {
            zoom = Clamp(zoom - 0.01f, 1, 5);
        }

        if (Keyboard.IsKeyDown(Keys.W))
        {
            offset += new Vector2(0, -1);
        }

        if (Keyboard.IsKeyDown(Keys.S))
        {
            offset += new Vector2(0, 1);
        }

        if (Keyboard.IsKeyDown(Keys.A))
        {
            offset += new Vector2(-1, 0);
        }

        if (Keyboard.IsKeyDown(Keys.D))
        {
            offset += new Vector2(1, 0);
        }

        var maxOffsetX = (size.X / 2f) * (1f - (1f / zoom));
        var maxOffsetY = (size.Y / 2f) * (1f - (1f / zoom));
        offset.X = Clamp(offset.X, -maxOffsetX, maxOffsetX);
        offset.Y = Clamp(offset.Y, -maxOffsetY, maxOffsetY);

        var center = new Vector2(size.X / 2f, size.Y / 2f);

        camera =
            Matrix.CreateTranslation(-offset.X, -offset.Y, 0) *
            Matrix.CreateTranslation(-center.X, -center.Y, 0) *
            Matrix.CreateScale(zoom) *
            Matrix.CreateTranslation(center.X, center.Y, 0);

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        if (texture == null)
        {
            return;
        }

        app.ScreenService.SetRenderTarget(frame);

        app.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                camera
        );
        app.SpriteBatch.Draw(texture, new Rectangle(0, 0, size.X, size.Y), Color.White);
        app.SpriteBatch.End();

        app.ScreenService.SetRenderTarget(null);

        app.SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone
        );
        app.SpriteBatch.Draw(frame, new Rectangle(0, 0, size.X, size.Y), Color.White);
        app.SpriteBatch.End();

        base.Draw(gameTime);
    }

    public void Regenerate() => GenerateTexture();

    private void GenerateTexture()
    {
        texture?.Dispose();
        var heightMap = MapService.BuildHeightMap(size.X, size.Y);
        texture = BuildTexture(heightMap);
        frame = new RenderTarget2D(GraphicsDevice, size.X, size.Y);
    }

    private Texture2D BuildTexture(float[,] heightMap)
    {
        var width = heightMap.GetLength(0);
        var height = heightMap.GetLength(1);
        var colors = new Color[width * height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                colors[y * width + x] = ColorForHeight(heightMap[x, y]);
            }
        }

        var texture = new Texture2D(GraphicsDevice, width, height);
        texture.SetData(colors);
        return texture;
    }

    private static Color ColorForHeight(float height)
    {
        var deepWater = new Color(0, 24, 64);
        var shallowWater = new Color(0, 96, 128);
        var sand = new Color(210, 200, 140);
        var grass = new Color(50, 130, 70);
        var rock = new Color(90, 90, 90);
        var snow = new Color(240, 240, 240);

        if (height <= 0.20f)
        {
            return deepWater;
        }

        if (height <= 0.35f)
        {
            return shallowWater;
        }

        if (height <= 0.45f)
        {
            return sand;
        }

        if (height <= 0.70f)
        {
            return grass;
        }

        if (height <= 0.85f)
        {
            return rock;
        }

        return snow;
    }
}
