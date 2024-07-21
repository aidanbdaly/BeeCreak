namespace BeeCreak.Run;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public struct LightSource
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
}

public class World : IWorld
{
    public Tile[,] TileSet { get; set; } = default!;
    public Vector2 SpawnPoint { get; set; } = default!;
    public List<Entity> Entities { get; set; } = default!;
    public int Size { get; set; } = default!;
    public int SizeInPixels => Size * Context.Static.TILE_SIZE;
    public RenderTarget2D SceneTarget { get; set; } = default!;
    public Vector2 MaxPosition { get; set; } = default!;
    public Texture2D LightMap { get; set; } = default!;
    public IContext Context { get; set; } = default!;

    public World(IContext context, int size)
    {
        Context = context;
        Size = size;

        var worldGenerator = new WorldGenerator(context, unchecked((int)DateTime.Now.Ticks), size);

        TileSet = worldGenerator.Generate();

        MaxPosition = new Vector2(
            SizeInPixels - context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width,
            SizeInPixels - context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height
        );

        SpawnPoint = MaxPosition / 2;

        List<LightSource> lightSources =
            new()
            {
                new() { Position = new Vector2(Size / 2, Size / 2), Radius = 150 }, // Sun
            };

        Entities = new List<Entity>
        {
            new Character(Context, CanMove, SpawnPoint),
            new Cursor(Context, TileSet),
        };

        SceneTarget = new RenderTarget2D(context.Static.GraphicsDevice, SizeInPixels, SizeInPixels);

        LightMap = new Texture2D(context.Static.GraphicsDevice, Size, Size);

        RecalculateLightMap(lightSources);

        Context.Static.GraphicsDevice.SetRenderTarget(SceneTarget);
        Context.Static.GraphicsDevice.Clear(Color.Black);

        Context.Static.SpriteController.Batch.Begin(
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                var tile = TileSet[x, y];
                Context.Static.SpriteController.Batch.Draw(
                    tile.Texture,
                    tile.Position,
                    Color.White
                );
            }
        }

        Context.Static.SpriteController.Batch.End();
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
        var camera = Context.Dynamic.Camera;

        Context.Static.GraphicsDevice.SetRenderTarget(null);

        Context.Static.SpriteController.Batch.Begin(
            transformMatrix: camera.Transform,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        Context.Static.SpriteController.Batch.Draw(SceneTarget, Vector2.Zero, Color.White);

        Context.Static.SpriteController.Batch.End();

        foreach (var entity in Entities)
        {
            entity.Draw();
        }

        Context.Static.SpriteController.Batch.Begin(
            blendState: Context.Static.SpriteController.Multiply,
            transformMatrix: camera.Transform
        );

        Context.Static.SpriteController.Batch.Draw(
            LightMap,
            Vector2.Zero,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Context.Static.TILE_SIZE,
            SpriteEffects.None,
            0f
        );

        Context.Static.SpriteController.Batch.End();
    }

    private void RecalculateLightMap(List<LightSource> lightSources)
    {
        Color[] colorData = new Color[Size * Size];

        LightMap.GetData(colorData);

        foreach (var light in lightSources)
        {
            foreach (var (x, y) in Graphic.Circle(light.Radius).Coordinates)
            {
                int X = (int)light.Position.X + x;
                int Y = (int)light.Position.Y + y;

                //float prevIntensity = colorData[X * Size + Y].A / 255f;

                float distanceToLight = Vector2.Distance(new Vector2(X, Y), light.Position);

                float intensity = MathHelper.Clamp(1 - distanceToLight / light.Radius, 0f, 1f);

                byte intensityByte = (byte)(intensity * 255);

                if (IsInWorld(X, Y) && !TileSet[X, Y].IsSolid)
                {
                    colorData[Y * Size + X] = new Color(
                        intensityByte,
                        intensityByte,
                        intensityByte,
                        (byte)255
                    );
                }
            }
        }

        LightMap.SetData(colorData);
    }

    private bool CanMove(float x, float y)
    {
        var X = (int)x / Context.Static.TILE_SIZE;
        var Y = (int)y / Context.Static.TILE_SIZE;

        var tile = TileSet[X, Y];

        return !tile.IsSolid && IsInWorld(X, Y);
    }

    private bool IsInWorld(int x, int y)
    {
        return x >= 0 && y >= 0 && x < Size && y < Size;
    }
}
