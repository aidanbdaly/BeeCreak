
using BeeCreak.Run.GameObjects.World.Entity.Delegates;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class EntityManager
{
    private RenderTarget2D Target { get; set; }
    private ICell Cell { get; set; }
    private CollisionDelegate Collision { get; set; }
    private IEventBus EventBus { get; set; }
    private IToolCollection Tools { get; set; }

    public EntityManager(
        IToolCollection tools,
        IEventBus eventBus,
        CollisionDelegate collision,
        ICell cell
    )
    {
        Tools = tools;
        EventBus = eventBus;
        Cell = cell;
        Collision = collision;

        var sizeInPixels = cell.Size * tools.Static.TILE_SIZE;

        Target = new RenderTarget2D(
            tools.Static.GraphicsDevice,
            sizeInPixels,
            sizeInPixels,
            false,
            SurfaceFormat.Color,
            DepthFormat.None,
            0,
            RenderTargetUsage.DiscardContents
        );

        EventBus.Subscribe<AddEntityEvent>(AddEntity);
        EventBus.Subscribe<RemoveEntityEvent>(RemoveEntity);

        Load();
    }

    private void AddEntity(AddEntityEvent e)
    {
        Cell.Entities.Add(e.Entity);
    }

    private void RemoveEntity(RemoveEntityEvent e)
    {
        Cell.Entities.Remove(e.Entity);
    }

    public void Load()
    {
        foreach (var entity in Cell.Entities)
        {
            entity.Collision = Collision;
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in Cell.Entities)
        {
            entity.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var entity in Cell.Entities)
        {
            entity.Draw();
        }
    }
}
