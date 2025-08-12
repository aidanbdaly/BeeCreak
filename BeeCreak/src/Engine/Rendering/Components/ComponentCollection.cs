using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class ComponentCollection<T> : IComponent, IBehavior where T : IComponent
    {
        protected readonly List<T> components;

        public ComponentCollection(IEnumerable<T> components)
        {
            foreach (var component in components)
            {
                component.Import(this);

                this.components.Add(component);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                if (component is IBehavior updatable)
                {
                    updatable.Update(gameTime);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.Deferred
            );

            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
