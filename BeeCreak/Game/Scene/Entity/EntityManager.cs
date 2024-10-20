using System;
using System.Collections.Generic;
using BeeCreak.Game.Objects.Camera;
using BeeCreak.Game.Scene.Entity.Events;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene.Entity;

public class EntityManager : IDynamicRenderable
{
    private string Name { get; set; }
    private List<Entity> Entities { get; set; }
    private List<(Action<Entity> action, Entity entity)> UpdateTasks { get; set; }
    private IToolCollection Tools { get; set; }

    public EntityManager(IToolCollection tools, List<Entity> entities, string name)
    {
        Tools = tools;
        Entities = entities;
        Name = name;

        UpdateTasks = new List<(Action<Entity> action, Entity entity)>();

        Tools.Static.Events.Listen<AddEntityEvent>(HandleAddEntity, Name);
        Tools.Static.Events.Listen<RemoveEntityEvent>(HandleRemoveEntity, Name);
    }

    private void HandleAddEntity(AddEntityEvent addEntityEvent)
    {
        var entity = addEntityEvent.Entity;

        UpdateTasks.Add(((entity) => Entities.Add(entity), entity));
    }

    private void HandleRemoveEntity(RemoveEntityEvent removeEntityEvent)
    {
        var entity = removeEntityEvent.Entity;

        UpdateTasks.Add(((entity) => Entities.Remove(entity), entity));
    }

    public void Update(GameTime gameTime)
    {
        foreach (var (action, entity) in UpdateTasks)
        {
            action.Invoke(entity);
        }

        UpdateTasks.Clear();

        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var entity in Entities)
        {
            entity.Draw();
        }
    }
}
