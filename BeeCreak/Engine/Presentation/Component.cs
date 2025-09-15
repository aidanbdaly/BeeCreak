using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BeeCreak.Engine.Core;

namespace BeeCreak.Engine.Presentation
{
    public class Transform
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Rotation { get; set; } = 0.0f;

        public float Scale { get; set; } = 1.0f;

        public Transform(
            Vector2 position = default,
            float rotation = 0.0f,
            float scale = 1.0f)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }

    public abstract class Component : IComponent
    {
        public bool IsEnabled { get; set; } = true;

        public Transform LocalTransform { get; set; } = new Transform();

        public Transform WorldTransform { get; private set; } = new Transform();

        public Color Color { get; set; } = Color.White;

        public Vector2 Origin { get; set; } = Vector2.Zero;

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public float LayerDepth { get; set; } = 0.0f;

        //its positioned absolute if it does not use a parents world transform and only relies on the local transform
        //do we need a separate method for only updating the local transform based on some basic parameters

        public void UpdateLocalTransform(Vector2? position = null, float? rotation = null, float? scale = null)
        {
            if (position.HasValue) { LocalTransform.Position = position.Value; }
            if (rotation.HasValue) { LocalTransform.Rotation = rotation.Value; }
            if (scale.HasValue) { LocalTransform.Scale = scale.Value; }
        }

        public void UpdateWorldTransform(Transform parentWorldTransform)
        {
            WorldTransform.Position = parentWorldTransform.Position + LocalTransform.Position;
            WorldTransform.Rotation = parentWorldTransform.Rotation + LocalTransform.Rotation;
            WorldTransform.Scale = parentWorldTransform.Scale * LocalTransform.Scale;
        }

        public abstract Rectangle GetBounds();

        public abstract void Dispose();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}