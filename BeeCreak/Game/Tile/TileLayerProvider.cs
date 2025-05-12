using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// provider might have been the wrong move (consider putting layermanager back inside here and calling this TileLayerManager)
public class TileLayerProvider 
{
    private readonly TileManager tileManager;

    private readonly AssetManager assetManager;

    public TileLayerProvider(TileManager tileManager, AssetManager assetManager)
    {
        this.assetManager = assetManager;
        this.tileManager = tileManager;

        tileManager.StateImported += OnStateImported;
    }

    private RenderTarget2D? TileMap { get; set; }

    private GraphicsDevice? GraphicsDevice { get; set; }

    private SpriteBatch? SpriteBatch { get; set; }


    public void Initialize(GraphicsDevice graphicsDevice)
    {
        GraphicsDevice = graphicsDevice;

        TileMap = new RenderTarget2D(
            graphicsDevice,
            EngineConfig.TILE_RESOLUTION * tileManager.Tiles.GetLength(0),
            EngineConfig.TILE_RESOLUTION * tileManager.Tiles.GetLength(1),
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.PreserveContents
        ); 
    }

    protected void OnStateImported(object? sender, EventArgs e)
    {
        GraphicsDevice?.SetRenderTarget(TileMap);

        SpriteBatch?.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend);

        var spriteSheet = assetManager.Get<SpriteSheet>("tiles");

        foreach (var tile in tileManager.Tiles)
        {
            SpriteBatch?.Draw(
                spriteSheet.GetSprite($"{tile.Type}_{tile.Variant}"),
                new Rectangle(
                    tile.Coordinate.X * EngineConfig.TILE_RESOLUTION,
                    tile.Coordinate.Y * EngineConfig.TILE_RESOLUTION,
                    EngineConfig.TILE_RESOLUTION,
                    EngineConfig.TILE_RESOLUTION
                ),
                Color.White);
        }

        SpriteBatch?.End();

        GraphicsDevice?.SetRenderTarget(null);
    }
}