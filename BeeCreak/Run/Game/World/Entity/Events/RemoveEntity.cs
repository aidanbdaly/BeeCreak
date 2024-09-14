namespace BeeCreak.Run.GameObjects.World.Entity.Events;

public class RemoveEntityEvent
{
    public MoveableEntity Entity { get; set; }

    public RemoveEntityEvent(MoveableEntity entity)
    {
        Entity = entity;
    }
}
