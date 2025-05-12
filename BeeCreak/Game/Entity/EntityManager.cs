using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class EntityManager
{
    public event EventHandler<EventArgs>? StateImported;

    public Dictionary<int, Vector2> Positions { get; private set; } = new();

    public Dictionary<int, float> Velocities { get; private set; } = new();

    public Dictionary<int, string> Sprites { get; private set; } = new();

    public Dictionary<int, string> PersistentId { get; private set; } = new();

    public Dictionary<int, Rectangle> Hitboxes { get; private set; } = new();

    public int Player { get; private set; }

    public static int NextId { get; private set; } = 0;

    public EntityState GetEntityState(int id)
    {
        return new EntityState()
        {
            Position = Positions[id],
            Velocity = Velocities[id],
            Sprite = Sprites[id],
            PersistentId = PersistentId[id],
            HitBox = Hitboxes[id],
            Controlled = id == Player,
        };
    }

    public EntityState GetPlayer()
    {
        return GetEntityState(Player);
    }

    public List<EntityState> Export()
    {
        var state = new List<EntityState>();

        foreach (var (id, _) in PersistentId)
        {
            state.Add(GetEntityState(id));
        }

        return state;
    }

    public void Import(List<EntityState> entityStates)
    {
        Positions.Clear();
        Velocities.Clear();
        Sprites.Clear();
        PersistentId.Clear();

        NextId = 0;

        foreach (var entityState in entityStates)
        {
            var id = NextId++;

            Positions[id] = entityState.Position;
            Velocities[id] = entityState.Velocity;
            Sprites[id] = entityState.Sprite;
            PersistentId[id] = entityState.PersistentId;

            if (entityState.Controlled)
            {
                Player = id;
            }
        }

        StateImported?.Invoke(this, EventArgs.Empty);
    }
}
