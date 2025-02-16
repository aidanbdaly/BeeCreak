using BeeCreak.Shared;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface IEntity : IGameObject
{
    EntityType Type { get; set; }

    EntityVariant Variant { get; set; }

    Vector2 Position { get; set; }

    float Speed { get; set; }

    void SetCollisionHandler(ICollisionHandler collisionHandler);

    void SetPosition(Vector2 position);

    void Move(Vector2 amount);

    void SetSpeed(float speed);

    void SetType(EntityType type);

    void SetVariant(EntityVariant variant);
}