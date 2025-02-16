using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Main;

public class CellController
{
    private readonly ISpriteController spriteController;

    private readonly IEntityController entityController;

    private readonly ITileMapController tileMapController;

    public CellController(ISpriteController spriteController)
    {
        this.spriteController = spriteController;
    }

    public void Load(ICell cell)
    {
        tileMapController.Load(cell.TileMap);
        entityController.Load(cell.Entities);
    }

    public void Update(GameTime gameTime)
    {
        entityController.Update(gameTime);
    }

    public void Draw(ICamera camera)
    {
        spriteController.GraphicsDevice.SetRenderTarget(null);

        spriteController.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend,
            transformMatrix: camera.ZoomTransform);

        tileMapController.Draw();
        entityController.Draw();

        spriteController.Batch.End();
    }
}