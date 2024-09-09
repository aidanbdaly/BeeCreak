namespace BeeCreak.Run.GameObjects.World.Entity.Events;

public class AddEntityEvent
{
    public Entity Entity { get; set; }

    public AddEntityEvent(Entity entity)
    {
        Entity = entity;
    }
}
