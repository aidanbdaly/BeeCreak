namespace BeeCreak.Run.Game.Scene.Entity.Events;

public class AddEntityEvent
{
    public Entity Entity { get; set; }
    public string CellName { get; set; }

    public AddEntityEvent(Entity entity, string cellName)
    {
        Entity = entity;
        CellName = cellName;
    }
}
