namespace BeeCreak.Game.Scene.Entity
{
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public abstract class EntityDTO
    {
        public EntityType Type { get; set; }

        public EntityVariant Variant { get; set; }

        public Vector2 WorldPosition { get; set; }

        public Direction Direction { get; set; }

        public float Speed { get; set; }
    }
}