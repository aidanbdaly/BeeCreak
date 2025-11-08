using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public class Component : IComponent
    {
        public bool IsEnabled { get; set; } = true;

        public virtual void Initialize() { }
    }

    public abstract class Updateable : Component, IUpdateable
    {
        public abstract void Update(GameTime gameTime);
    }

    public abstract class Renderable : Component, IRenderable
    {
        public Vector2 Position { get; set; } = Vector2.Zero;

        public float Rotation { get; set; } = 0f;

        public float Scale { get; set; } = 1f;

        public Color Color { get; set; } = Color.White;

        public Vector2 Origin { get; set; } = Vector2.Zero;

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public float LayerDepth { get; set; } = 0.0f;

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract Rectangle GetBounds();

        public abstract void Dispose();
    }
}
