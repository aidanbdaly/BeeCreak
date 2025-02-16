using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class EntityDTO
{
    public EntityType Type { get; set; }

    public EntityVariant Variant { get; set; }

    public Vector2 Position { get; set; }

    public float Speed { get; set; }
}