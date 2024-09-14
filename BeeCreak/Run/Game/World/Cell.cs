using System;
using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.GameObjects.World.Tile;
using BeeCreak.Run.Generation;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.World;

public class Cell : ICell
{
    public string Name { get; set; }
    public ITile[,] Map { get; set; }
    public int Size { get; set; }
    public int SizeInPixels { get; set; }
    public List<IEntity> Entities { get; set; }
    public List<ILight> Lights { get; set; }
    public Vector2 SpawnPoint { get; set; }
    private List<(Action<IEntity> action, IEntity entity)> UpdateTasks { get; set; }
    private IEventManager EventManager { get; set; }
    private IToolCollection Tools { get; set; }

    public Cell(
        IToolCollection tools,
        IEventManager eventManager,
        string name,
        int size,
        Vector2 spawnPoint
    )
    {
        Tools = tools;
        EventManager = eventManager;

        Name = name;
        Size = size;
        SizeInPixels = size * Tools.Static.TILE_SIZE;
        SpawnPoint = spawnPoint;

        Entities = new List<IEntity>();
        Lights = new List<ILight>();
        UpdateTasks = new List<(Action<IEntity> action, IEntity entity)>();

        var shapeRouter = new ShapeRouter(tools, 12, size);

        Map = shapeRouter.Route();

        EventManager.Listen<AddEntityEvent>(HandleAddEntity, Name);
        EventManager.Listen<RemoveEntityEvent>(HandleRemoveEntity, Name);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var (action, entity) in UpdateTasks)
        {
            action.Invoke(entity);
        }

        UpdateTasks.Clear();
    }

    private void HandleAddEntity(AddEntityEvent addEntityEvent)
    {
        var entity = addEntityEvent.Entity;

        entity.Collision = Collision;
        entity.WorldPosition = SpawnPoint * Tools.Static.TILE_SIZE + new Vector2(16, 0);

        UpdateTasks.Add((AddEntity, entity));
    }

    private void HandleRemoveEntity(RemoveEntityEvent removeEntityEvent)
    {
        var entity = removeEntityEvent.Entity;

        UpdateTasks.Add((RemoveEntity, entity));
    }

    private void AddEntity(IEntity entity)
    {
        Entities.Add(entity);
    }

    private void RemoveEntity(IEntity entity)
    {
        Entities.Remove(entity);
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
            var tile = Map[x, y];

            if (tile.Bounds.Intersects(bounds))
            {
                return true;
            }
        }

        return false;
    }
}
