using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TileLayerComponent : Component
{
    private readonly TileManager tileManager;

    private readonly Camera camera;

    private readonly IGraphicsDeviceService graphicsDevice;

    public TileLayerComponent(IGraphicsDeviceService graphicsDevice, TileManager tileManager, Camera camera)
    {
        this.graphicsDevice = graphicsDevice;
        this.tileManager = tileManager;
        this.camera = camera;

        tileManager.OnStateImported += (sender, e) =>
        {
            StateImported(sender, e);
        };

        tileManager.TileVariantChanged += (sender, e) =>
        {
            RedrawRequired = true;
        };
    }

    private RenderTarget2D TileMap { get; set; }

    private SpriteSheet SpriteSheet { get; set; }

    private bool RedrawRequired { get; set; } = true;

    public override void LoadContent(AssetManager assetManager)
    {
        SpriteSheet = assetManager.Load<SpriteSheet>("Spritesheet/tile");
    }

    public override void UnloadContent(AssetManager assetManager)
    {
        TileMap?.Dispose();
        TileMap = null;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (RedrawRequired)
        {
            graphicsDevice.GraphicsDevice.SetRenderTarget(TileMap);

            graphicsDevice.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend);

            foreach (var (x, y, tile) in tileManager.Enumerate())
            {
                if (tile != null)
                {
                    spriteBatch.Draw(
                        SpriteSheet?.GetSprite($"{tile.State.Type}_{tile.State.Variant}"),
                        new Rectangle(
                            x * EngineConfig.TILE_RESOLUTION,
                            y * EngineConfig.TILE_RESOLUTION,
                            EngineConfig.TILE_RESOLUTION,
                            EngineConfig.TILE_RESOLUTION
                        ),
                        Color.White);
                }
            }

            spriteBatch.End();

            graphicsDevice.GraphicsDevice.SetRenderTarget(null);
            RedrawRequired = false;
        }

        graphicsDevice.GraphicsDevice.Clear(Color.Transparent);

        spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend,
            sortMode: SpriteSortMode.Deferred,
            transformMatrix: camera.Transform
            );

        spriteBatch.Draw(
            TileMap,
            Vector2.Zero,
            Color.White);

        spriteBatch.End();
    }

    private void StateImported(object sender, EventArgs e)
    {
        TileMap = new RenderTarget2D(
        graphicsDevice.GraphicsDevice,
        EngineConfig.TILE_RESOLUTION * tileManager.Width,
        EngineConfig.TILE_RESOLUTION * tileManager.Height,
        false,
        SurfaceFormat.Color,
        DepthFormat.None,
        0,
        RenderTargetUsage.PreserveContents);

        RedrawRequired = true;
    }
}