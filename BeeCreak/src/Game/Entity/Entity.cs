public class Entity
{
    public Entity(EntityState state, EntityAttributes attributes)
    {
        State = state;
        Attributes = attributes;
    }

    public EntityState State { get; }

    public EntityAttributes Attributes { get; }
}

