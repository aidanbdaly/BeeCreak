using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public interface IComponent : Core.IDrawable, IDisposable
    {
        bool IsEnabled { get; set; }

        Transform LocalTransform { get; set; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }

        Rectangle GetBounds();

        void UpdateLocalTransform(Vector2? position = null, float? rotation = null, float? scale = null);
    }
}
