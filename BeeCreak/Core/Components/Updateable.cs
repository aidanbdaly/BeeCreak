using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Components
{
    public abstract class Updateable : Component, IUpdateable
    {
        public abstract void Update(GameTime gameTime);
    }
}