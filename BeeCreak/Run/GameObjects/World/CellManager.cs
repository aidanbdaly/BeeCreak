using System;
using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.GameObjects.World.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World;

public class CellManager : IGameObject
{
    private TileManager TileManager { get; set; } = default!;
    private LightManager LightManager { get; set; } = default!;
    private EntityManager EntityManager { get; set; } = default!;
    private ICell Cell { get; set; } = default!;
    private IEventBus EventBus { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public CellManager(IToolCollection tools, IEventBus eventBus, ICell cell)
    {
        Tools = tools;
        Cell = cell;

        TileManager = new TileManager(Tools, cell);
        LightManager = new LightManager(Tools, IsOpaque, cell);
        EntityManager = new EntityManager(Tools, eventBus, Collision, cell);
    }

    private bool Collision(Vector2 position, Rectangle bounds)
    {
        var X = (int)Math.Round(position.X) / Tools.Static.TILE_SIZE;
        var Y = (int)Math.Round(position.Y) / Tools.Static.TILE_SIZE;

        var tilesToTestForIntersection = new List<(int, int)>
        {
            (X, Y),
            (X, Y + 1),
            (X, Y - 1),
            (X + 1, Y),
            (X - 1, Y),
            (X + 1, Y + 1),
            (X - 1, Y - 1),
            (X + 1, Y - 1),
            (X - 1, Y + 1),
        };

        foreach (var (x, y) in tilesToTestForIntersection)
        {
            var tile = Cell.Map[x, y];

            if (tile.Bounds.Intersects(bounds))
            {
                return true;
            }
        }

        return false;
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
