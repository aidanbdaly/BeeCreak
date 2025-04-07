using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class EntityManager
{
    public event EventHandler<EventArgs>? StateImported;

    public EntityManager() {}

    public Dictionary<int, Vector2> Positions { get; private set; }

    public Dictionary<int, float> Velocities { get; private set; }

    public Dictionary<int, string> Sprites { get; private set; }

    public Dictionary<int, string> PersistentId { get; private set; }

    public void UpdateEntityPosition(int id, Vector2 position)
    {
        Positions[id] = position;
    }

    public void UpdateEntityVelocity(int id, float velocity)
    {
        Velocities[id] = velocity;
    }

    public void UpdateEntitySprite(int id, string sprite)
    {
        Sprites[id] = sprite;
    }

    public void UpdateEntityControlled(int id, string persistentId)
    {
        PersistentId[id] = persistentId;
    }

    public void Clear()
    {
        Positions.Clear();
        Velocities.Clear();
        Sprites.Clear();
        PersistentId.Clear();

        Entity.ResetNextId();
    }

    public List<EntityState> ExportState()
    {
        var state = new List<EntityState>();

        foreach (var (id, persistentId) in PersistentId)
        {
            state.Add(
                new EntityState()
                {
                    Position = Positions[id],
                    Velocity = Velocities[id],
                    Sprite = Sprites[id],
                    PersistentId = persistentId
                }
            );
        }

        return state;
    }

    public void ImportState(List<EntityState> entityStates)
    {
        foreach (var entityState in entityStates)
        {
            var entity = new Entity();

            Positions[entity.Id] = entityState.Position;
            Velocities[entity.Id] = entityState.Velocity;
            Sprites[entity.Id] = entityState.Sprite;
            PersistentId[entity.Id] = entityState.PersistentId;
        }

        StateImported?.Invoke(this, EventArgs.Empty);
    }
}




// public void Move(Vector2 amount)
// {
//     if (CollisionHandler != null)
//     {
//         if (CollisionHandler.CanMoveBy(amount, BoundingBox))
//         {
//             Position += amount;
//             BoundingBox.Offset(amount);
//         }
//     }
//     else
//     {
//         Position += amount;
//         BoundingBox.Offset(amount);
//     }
// }