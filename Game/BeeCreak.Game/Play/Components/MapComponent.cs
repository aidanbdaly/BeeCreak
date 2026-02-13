using BeeCreak.Engine;
using BeeCreak.Engine.Services;
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
    Uint2D size,
    int? seed = null) : DrawableGameComponent(app)
{
    private readonly Point size = size.ToPoint();

    private readonly MapTextureService textureService = new(seed);

    private Texture2D? texture;

    private RenderTarget2D? frame;

    private Matrix camera;

    private Vector2 offset = Vector2.Zero;

    private float zoom = 1f;

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
        if (Keyboard.IsKeyDown(Keys.OemPlus))
        {
            float clamp(float value, float minValue, float maxValue)
            {
                if (value < minValue) return minValue;
                if (value > maxValue) return maxValue;
                return value;
            }

            zoom = clamp(zoom + 0.01f, 1, 5);
        }

        if (Keyboard.IsKeyDown(Keys.OemMinus))
        {
            float clamp(float value, float minValue, float maxValue)
            {
                if (value < minValue) return minValue;
                if (value > maxValue) return maxValue;
                return value;
            }

            zoom = clamp(zoom - 0.01f, 1, 5);
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
        texture = textureService.Build(GraphicsDevice, size.X, size.Y, out var map);
        frame = new RenderTarget2D(GraphicsDevice, size.X, size.Y);
    }
}
