using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Components
{
    public abstract class Updateable : IUpdateable
    {
        public abstract void Update(GameTime gameTime);
    }
}