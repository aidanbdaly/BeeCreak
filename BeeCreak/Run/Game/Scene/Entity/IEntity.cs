using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Entity;

public interface IEntity : IDynamicDrawable
{
    EntityType EntityType { get; set; }
    Vector2 WorldPosition { get; set; }
    Direction Direction { get; set; }
    float Speed { get; set; }
    void SetCollisionHandler(ICollisionHandler collisionHandler);
    EntityDTO ToDTO();
}
