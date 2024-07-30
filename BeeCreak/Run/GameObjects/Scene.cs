using System.Collections.Generic;
using BeeCreak.Run.GameObjects;
using BeeCreak.Run.GameObjects.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Generation;

public class Scene : IGameObject
{
    private Vector2 SpawnPoint { get; set; } = default!;
    private List<Entity> Entities { get; set; } = default!;
    private List<LightSource> LightSources { get; set; } = default!;
    private TileManager TileManager { get; set; } = default!;
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

        LightSources = new()
        {
            new() { Position = new Vector2(size / 2, size / 2), Radius = 150 }, // Sun
        };

        TileManager = new TileManager(Tools, size, 0, LightSources);

        Entities = new()
        {
            new Character(Tools, CanMove, SpawnPoint),
            new Cursor(Tools, HandleTileChange, TileManager.TileSet),
        };
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public void Draw()
    {
        var camera = Tools.Dynamic.Camera;

        var sourceRectangle = new Rectangle
        {
            X = (int)camera.WorldPosition.X,
            Y = (int)camera.WorldPosition.Y,
            Width = camera.ViewPortWidth,
            Height = camera.ViewPortHeight
        };

        var destinationRectangle = new Rectangle
        {
            X = 0,
            Y = 0,
            Width = camera.ViewPortWidth,
            Height = camera.ViewPortHeight
        };

        Tools.Static.GraphicsDevice.SetRenderTarget(null);

        Tools.Static.Sprite.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend,
            transformMatrix: camera.ZoomTransform
        );

        Tools.Static.Sprite.Batch.Draw(
            TileManager.SceneTarget,
            destinationRectangle,
            sourceRectangle,
            Color.White
        );

        Tools.Static.Sprite.Batch.End();

        foreach (var entity in Entities)
        {
            entity.Draw();
        }

        Tools.Static.Sprite.Batch.Begin(
            blendState: Tools.Static.Sprite.Multiply,
            transformMatrix: camera.Transform
        );

        Tools.Static.Sprite.Batch.Draw(
            TileManager.LightMap,
            Vector2.Zero,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Tools.Static.TILE_SIZE,
            SpriteEffects.None,
            0f
        );

        Tools.Static.Sprite.Batch.End();
    }

    private void HandleTileChange(int x, int y)
    {
        TileManager.UpdateLightMapPoint(x, y);
        TileManager.RedrawTile(x, y);
    }

    private bool CanMove(float x, float y)
    {
        var X = (int)x / Tools.Static.TILE_SIZE;
        var Y = (int)y / Tools.Static.TILE_SIZE;

        var tile = TileManager.TileSet[X, Y];

        return !tile.IsSolid && TileManager.IsInWorld(X, Y);
    }
}
