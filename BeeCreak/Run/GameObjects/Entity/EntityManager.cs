using System.Collections.Generic;
using BeeCreak.Run.GameObjects.Delegates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.Entity;

public class EntityManager
{
    public List<Entity> Entities { get; set; }
    public RenderTarget2D EntityTarget { get; set; }
    private IToolCollection Tools { get; set; }

    public EntityManager(
        ClampDelegate isTraversable,
        IToolCollection tools,
        Vector2 spawnPoint,
        int sizeInPixels
    )
    {
        Tools = tools;

        Entities = new()
        {
            new Character(Tools, isTraversable, spawnPoint),
            new Creature(Tools, isTraversable, spawnPoint),
        };

        EntityTarget = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            sizeInPixels,
            sizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.DiscardContents
        );
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }

        Tools.Static.GraphicsDevice.SetRenderTarget(EntityTarget);

        Tools.Static.GraphicsDevice.Clear(Color.Transparent);

        Tools.Static.Sprite.Batch.Begin(
            sortMode: SpriteSortMode.Deferred,
            blendState: BlendState.AlphaBlend
        );

        foreach (var entity in Entities)
        {
            entity.Draw();
        }

        Tools.Static.Sprite.Batch.End();
    }
}
