
namespace BeeCreak.Game.Scene.Entity;
public struct EntityVariant
{
    public EntityVariant(int id)
    {
        Id = id;
    }

    public static EntityVariant FacingLeft => new(0);

    public static EntityVariant FacingUp => new(1);

    public static EntityVariant FacingRight => new(2);

    public static EntityVariant FacingDown => new(3);

    public int Id { get; set; }
}