using System.Collections.Generic;
using BeeCreak.Run.GameObjects.Entity;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects;

public class Scene : IGameObject
{
    private TileManager TileManager { get; set; } = default!;
    private LightManager LightManager { get; set; } = default!;
    private EntityManager EntityManager { get; set; } = default!;
    private Vector2 SpawnPoint { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public Scene(IToolCollection tools, int size)
    {
        Tools = tools;

        var sizeInPixels = size * Tools.Static.TILE_SIZE;

        var maxPosition = new Vector2(
            sizeInPixels - tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width,
            sizeInPixels - tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height
        );

        SpawnPoint = maxPosition / 2;

        TileManager = new TileManager(Tools, size, sizeInPixels, 0);
        LightManager = new LightManager(Tools, IsOpaque, size, sizeInPixels);
        EntityManager = new EntityManager(Clamp, Tools, SpawnPoint, sizeInPixels);
    }

    private bool Clamp(Vector2 position, Rectangle bounds, Direction direction)
    {
        var tilesToTestForIntersection = new List<(int, int)> { };

        var X = (int)position.X / Tools.Static.TILE_SIZE;
        var Y = (int)position.Y / Tools.Static.TILE_SIZE;

        tilesToTestForIntersection.Add((X, Y));
        tilesToTestForIntersection.Add((X, Y + 1));
        tilesToTestForIntersection.Add((X, Y - 1));
        tilesToTestForIntersection.Add((X + 1, Y));
        tilesToTestForIntersection.Add((X - 1, Y));
        tilesToTestForIntersection.Add((X + 1, Y + 1));
        tilesToTestForIntersection.Add((X - 1, Y - 1));
        tilesToTestForIntersection.Add((X + 1, Y - 1));
        tilesToTestForIntersection.Add((X - 1, Y + 1));

        var isInWorld = X >= 0 && Y >= 0 && X < TileManager.Size && Y < TileManager.Size;

        foreach (var (x, y) in tilesToTestForIntersection)
        {
            if (isInWorld)
            {
                var tile = TileManager.TileSet[x, y];

                if (tile.Bounds.Intersects(bounds))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsOpaque(int X, int Y)
    {
        var tile = TileManager.TileSet[X, Y];

        var isInWorld = X >= 0 && Y >= 0 && X < TileManager.Size && Y < TileManager.Size;

        return isInWorld && tile.Opaque;
    }

    public void Update(GameTime gameTime)
    {
        EntityManager.Update(gameTime);
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

        Tools.Static.Sprite.Batch.Draw(
            TileManager.SceneTarget,
            camera.Destination,
            camera.Source,
            Color.White
        );

        Tools.Static.Sprite.Batch.Draw(
            EntityManager.EntityTarget,
            camera.Destination,
            camera.Source,
            Color.White
        );

        Tools.Static.Sprite.Batch.End();

        // Tools.Static.Sprite.Batch.Begin(
        //     samplerState: SamplerState.PointClamp,
        //     blendState: Tools.Static.Sprite.Multiply,
        //     transformMatrix: camera.ZoomTransform
        // );

        // Tools.Static.Sprite.Batch.Draw(
        //     LightManager.LightTarget,
        //     camera.Destination,
        //     camera.Source,
        //     Color.White
        // );

        // Tools.Static.Sprite.Batch.End();
    }
}
