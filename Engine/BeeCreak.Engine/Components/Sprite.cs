using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Graphics
{
    public abstract class Sprite(App app) : DrawableGameComponent(app)
    {
        public abstract Rectangle BoundingBox { get; }
    }
}