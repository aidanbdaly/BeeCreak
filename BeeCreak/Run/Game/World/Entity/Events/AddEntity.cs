namespace BeeCreak.Run.GameObjects.World.Entity.Events;

public class AddEntityEvent
{
    public IEntity Entity { get; set; }
    public string CellName { get; set; }

    public AddEntityEvent(MoveableEntity entity, string cellName)
    {
        Entity = entity;
        CellName = cellName;
    }
}
