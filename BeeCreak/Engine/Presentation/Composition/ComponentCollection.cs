using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Presentation.Composition
{
    public class ComponentCollection<T> : Component where T : IComponent
    {
        protected readonly List<T> components;

        public ComponentCollection(IEnumerable<T> components = null)
        {
            this.components = components?.ToList() ?? [];
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.UpdateWorldTransform(WorldTransform);
                component.Update(gameTime);
            }
        }

        public override void Dispose()
        {
            foreach (var component in components)
            {
                component.Dispose();
            }
        }

        public override Rectangle GetBounds()
        {
            var bounds = Rectangle.Empty;

            foreach (var component in components)
            {
                bounds = Rectangle.Union(bounds, component.GetBounds());
            }

            return bounds;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
        }
    }
}
