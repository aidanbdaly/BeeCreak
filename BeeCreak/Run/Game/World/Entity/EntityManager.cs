using System;
using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World.Entity;

public class EntityManager : IDynamicDrawable
{
    private ICell Cell { get; set; }
    private IEventManager EventBus { get; set; }
    private IToolCollection Tools { get; set; }

    public EntityManager(IToolCollection tools, IEventManager eventBus, ICell cell)
    {
        Tools = tools;
        EventBus = eventBus;
        Cell = cell;
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
