namespace BeeCreak;

public class EntityManager
{
    public event EventHandler<EventArgs> StateImported;

    private readonly CellManager cellManager;

    private readonly EntityFactory entityFactory;

    public EntityManager(CellManager cellManager, EntityFactory entityFactory)
    {
        this.cellManager = cellManager;
        this.entityFactory = entityFactory;

        cellManager.CellMounted += (sender, args) => HandleActiveCellChanged(args);
    }

    public Dictionary<string, Entity> Entities { get; } = [];

    private string Player { get; set; }

    public Entity PlayerEntity => Entities[Player];

    private void HandleActiveCellChanged(CellMountedEvent e)
    {
        Entities.Clear();

        for (int i = 0; i < e.Cell.Blueprint.Asset.EntityState.Count; i++)
        {
            var state = e.Cell.Blueprint.Asset.EntityState[i];
            var id = $"{state.ContentId}_{i}";

            Entities[id] = entityFactory.CreateEntity(state);

            if (state.ContentId == "player")
            {
                Player = id;
            }
        }

        if (e.Cell.State.EntityState != null)
        {
            foreach (var (id, state) in e.Cell.State.EntityState)
            {
                Entities[id] = entityFactory.CreateEntity(state);

                if (state.ContentId == "player")
                {
                    Player = id;
                }
            }
        }

        StateImported?.Invoke(this, EventArgs.Empty);
    }
}
