using BeeCreak.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene.Entity;

public abstract class EntityDTO
{
    public EntityType EntityType { get; set; }
    public Vector2 WorldPosition { get; set; }
    public Direction Direction { get; set; }
    public float Speed { get; set; }
    public abstract Entity FromDTO(IToolCollection tools);
}
