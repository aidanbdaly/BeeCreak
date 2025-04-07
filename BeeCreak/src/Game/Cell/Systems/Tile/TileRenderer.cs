using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TileRenderer
{
    private readonly SceneManager sceneManager;

    private readonly TileManager tileManager;

    public TileRenderer(SceneManager sceneManager, TileManager tileManager)
    {
        this.sceneManager = sceneManager;
        this.tileManager = tileManager;

        tileManager.TileChanged += OnTileChanged;
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

        sceneManager.AddLayer(new Layer()
        {
            Name = "TileMap",
            Content = new List<BasicElement>() {
                new ()
                {
                    Name = "TileMap",
                    Texture = TileMap,
                    Position = Vector2.Zero
                }
             },
            ZIndex = 0
        });
    }

    protected virtual void OnTileChanged(object? sender, EventArgs e)
    {
        Draw();
    }

    private void Draw()
    {
        GraphicsDevice?.SetRenderTarget(TileMap);

        SpriteBatch?.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend);

        var spriteSheet = AssetManager.Get<SpriteSheet>("tiles");

        foreach (var tile in tileManager.Tiles)
        {
            SpriteBatch?.Draw(
                spriteSheet.GetSprite($"{tile.Type}_{tile.Variant}"),
                tile.Position,
                Color.White);
        }

        SpriteBatch?.End();

        GraphicsDevice?.SetRenderTarget(null);
    }
}