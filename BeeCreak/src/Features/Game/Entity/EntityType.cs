namespace BeeCreak.Game.Scene.Entity;
public struct EntityType
{
    public EntityType(int id, bool hasVariant)
    {
        Id = id;
        HasVariant = hasVariant;
    }

    public static EntityType Character => new(0, true);
    public static EntityType Creature => new(1, false);
    public bool HasVariant { get; set; }
    public int Id { get; set; }
}