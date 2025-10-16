using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    // componentcollection and cachedcomponentcollection
    public class ComponentCollection<T> : Component where T : Component
    {
        protected List<T> components = new();

        public ComponentCollection() { }

        public ComponentCollection(IEnumerable<T> components)
        {
            if (components != null)
                this.components.AddRange(components);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.UpdateWorldTransform(WorldTransform);
                component.Update(gameTime);
            }
        }

        public override void Dispose() => components.ForEach(c => c.Dispose());

        public override Rectangle GetBounds()
        {
            var bounds = Rectangle.Empty;

            foreach (var component in components)
            {
                bounds = Rectangle.Union(bounds, component.GetBounds());
            }

            return bounds;
        }

        public override void Draw(SpriteBatch spriteBatch) => components.ForEach(c => c.Draw(spriteBatch));
    }
}
