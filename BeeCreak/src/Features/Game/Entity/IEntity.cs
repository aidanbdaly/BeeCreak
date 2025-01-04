namespace BeeCreak.Game.Scene.Entity
{
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public interface IEntity
    {
        EntityType Type { get; set; }

        EntityVariant Variant { get; set; }

        Vector2 WorldPosition { get; set; }

        Direction Direction { get; set; }

        float Speed { get; set; }

        bool IsMoving { get; set; }

        void Update(GameTime gameTime);
        
        void SetCollisionHandler(ICollisionHandler collisionHandler);
    }
}
