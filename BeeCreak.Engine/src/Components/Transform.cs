using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Components
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
}