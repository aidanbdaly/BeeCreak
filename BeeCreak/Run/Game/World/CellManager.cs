using System;
using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.GameObjects.World.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World;

public class CellManager : IDynamicDrawable
{
    private TileManager TileManager { get; set; } = default!;
    private LightManager LightManager { get; set; } = default!;
    private EntityManager EntityManager { get; set; } = default!;
    private ICell Cell { get; set; } = default!;
    private IEventManager EventBus { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public CellManager(IToolCollection tools, IEventManager eventBus, ICell cell)
    {
        Tools = tools;
        Cell = cell;

        TileManager = new TileManager(Tools, cell);
        LightManager = new LightManager(Tools, IsOpaque, cell);
        EntityManager = new EntityManager(Tools, eventBus, cell);
    }

    private bool IsOpaque(int X, int Y)
    {
        var tile = Cell.Map[X, Y];

        return IsInWorld(X, Y) && tile.Opaque;
    }

    private bool IsInWorld(int X, int Y)
    {
        return X >= 0 && Y >= 0 && X < Cell.Size && Y < Cell.Size;
    }

    public void Update(GameTime gameTime)
    {
        Cell.Update(gameTime);
        EntityManager.Update(gameTime);
        LightManager.Update(gameTime);
    }

    public void Draw()
    {
        var camera = Tools.Dynamic.Camera;

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
