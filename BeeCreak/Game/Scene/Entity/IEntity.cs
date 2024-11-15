namespace BeeCreak.Game.Scene.Entity
{
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public interface IEntity : IGameObject
    {
        EntityType Type { get; set; }

        Vector2 WorldPosition { get; set; }

        Direction Direction { get; set; }

        float Speed { get; set; }

        void SetCollisionHandler(ICollisionHandler collisionHandler);
    }
}
