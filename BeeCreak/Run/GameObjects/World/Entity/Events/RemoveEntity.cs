namespace BeeCreak.Run.GameObjects.World.Entity.Events;

public class RemoveEntityEvent
{
    public Entity Entity { get; set; }

    public RemoveEntityEvent(Entity entity)
    {
        Entity = entity;
    }
}
