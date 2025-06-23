using BeeCreak.Shared.Services;

public class EntityFactory
{
    private readonly AssetManager assetManager;

    public EntityFactory(AssetManager assetManager)
    {
        this.assetManager = assetManager;
    }

    public Entity CreateEntity(EntityState state, string id)
    {
        var attributes = assetManager.Load<EntityAttributes>($"Entity/{id}");

        return new Entity
        {
            State = state,
            Attributes = attributes
        };
    }
}