using BeeCreak.Run.Game.Objects.Camera;
using BeeCreak.Run.Game.Scene.Entity;
using BeeCreak.Run.Game.Scene.Light;
using BeeCreak.Run.Game.Scene.Tile;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Game.Scene;

public class CellManager
{
    private TileManager TileManager { get; set; } = default!;
    private LightManager LightManager { get; set; } = default!;
    private EntityManager EntityManager { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public CellManager(IToolCollection tools, ICell cell)
    {
        Tools = tools;

        TileManager = new TileManager(Tools, cell.Tiles, cell.Size);
        LightManager = new LightManager(Tools, cell.Lights, cell.Size);
        EntityManager = new EntityManager(Tools, cell.Entities, cell.Name);
    }

    public void Update(GameTime gameTime)
    {
        EntityManager.Update(gameTime);
        LightManager.Update(gameTime);
    }

    public void Draw(ICamera camera)
    {
        Tools.Static.GraphicsDevice.SetRenderTarget(null);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend,
            transformMatrix: camera.ZoomTransform
        );

        TileManager.Draw();
        EntityManager.Draw();

        Tools.Static.Sprite.Batch.End();

        if (LightManager.RenderLighting)
        {
            Tools.Static.Sprite.Batch.Begin(
                blendState: Tools.Static.Sprite.Multiply,
                transformMatrix: camera.ZoomTransform
            );

            LightManager.Draw();

            Tools.Static.Sprite.Batch.End();
        }
    }
}
