using Microsoft.Xna.Framework;

public class EntityFactory
{
    public EntityState fromDTO(EntityStateDTO entityState)
    {
        return new EntityState()
        {
            PersistentId = entityState.PersistentId,
            Position = new Vector2(entityState.Position.X, entityState.Position.Y),
            Velocity = entityState.Velocity,
            Sprite = entityState.Sprite
        };
    }

    public EntityStateDTO toDTO(EntityState entityState)
    {
        return new EntityStateDTO()
        {
            PersistentId = entityState.PersistentId,
            Position = new Vector2DTO()
            {
                X = entityState.Position.X,
                Y = entityState.Position.Y
            },
            Velocity = entityState.Velocity,
            Sprite = entityState.Sprite
        };
    }
}