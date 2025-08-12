using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class ComponentSelector<T> : IDrawable, IBehavior where T : IComponent
    {
        protected Dictionary<string, T> Components = [];

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
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

            foreach (var (_, component) in Components)
            {
                component.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}