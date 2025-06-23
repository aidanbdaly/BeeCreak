using BeeCreak.Shared.Services;

namespace BeeCreak.Scene.Main;

public class EntityManager
{
    public event EventHandler<EventArgs> StateImported;

    private readonly CellManager cellManager;

    public EntityManager(CellManager cellManager)
    {
        this.cellManager = cellManager;

        cellManager.StateImported += (sender, args) => Import(cellManager.CurrentCell.State.Entities);
    }

    private Dictionary<int, Entity> Entities { get; } = [];

    private int Player { get; set; } = -1;

    private int NextId { get; set; } = 0;

    public Entity GetPlayer()
    {
        return GetEntity(Player);
    }

    public Entity GetEntity(int id)
    {
        return Entities.TryGetValue(id, out var entity) ? entity : null;
    }

    public void Import(List<EntityState> entities)
    {
        foreach (var state in entities)
        {
            Entities[NextId++] = EntityFactory.CreateEntity(state, NextId.ToString());

            if (state.Type == "Player")
            {
                Player = NextId;
            }
        }

        StateImported?.Invoke(this, EventArgs.Empty);
    }
}
