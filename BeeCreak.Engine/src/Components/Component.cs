using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public abstract class Component : IComponent
    {
        public Transform LocalTransform { get; set; } = new();
        public Transform WorldTransform { get; private set; } = new();
        public bool IsEnabled { get; set; } = true;
        public Color Color { get; set; } = Color.White;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; } = 0.0f;

        public void UpdateWorldTransform(Transform parentWorldTransform)
        {
            WorldTransform.Position = parentWorldTransform.Position + LocalTransform.Position;
            WorldTransform.Rotation = parentWorldTransform.Rotation + LocalTransform.Rotation;
            WorldTransform.Scale = parentWorldTransform.Scale * LocalTransform.Scale;
        }

        public void UpdateLocalTransform(Vector2? position = null, float? rotation = null, float? scale = null)
        {
            if (position.HasValue) { LocalTransform.Position = position.Value; }
            if (rotation.HasValue) { LocalTransform.Rotation = rotation.Value; }
            if (scale.HasValue) { LocalTransform.Scale = scale.Value; }
        }
        public virtual void Update(GameTime gameTime) { }
        public abstract Rectangle GetBounds();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Dispose();
    }
}