namespace BeeCreak.Game.Scene.Entity
{
    using global::BeeCreak.Config;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;

    public abstract class Entity : IEntity
    {
        public EntityType Type { get; set; }

        public EntityVariant Variant { get; set; }

        public Direction Direction { get; set; }

        public Vector2 WorldPosition { get; set; }

        public float Speed { get; set; }

        public bool IsMoving { get; set; }

        protected ICollisionHandler CollisionHandler { get; set; }

        public abstract void Update(GameTime gameTime);

        public void SetPosition(Vector2 position)
        {
            WorldPosition = position;
        }

        public void SetCollisionHandler(ICollisionHandler collisionHandler)
        {
            CollisionHandler = collisionHandler;
        }

        protected void Move(Direction direction, GameTime gameTime)
        {
            var newPosition =
                WorldPosition
                + (Vector2.Normalize(direction.Value)
                    * ((float)(Speed * gameTime.ElapsedGameTime.TotalSeconds)));

            var boundingBox = new Rectangle(
                (int)newPosition.X + 10,
                (int)newPosition.Y,
                Globals.TileSize - 20,
                Globals.TileSize);

            if (CollisionHandler.CheckCollision(newPosition, boundingBox))
            {
                return;
            }

            WorldPosition = newPosition;
        }
    }
}
