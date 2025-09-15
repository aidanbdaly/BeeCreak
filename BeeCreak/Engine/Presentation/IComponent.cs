using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BeeCreak.Engine.Core;

namespace BeeCreak.Engine.Presentation
{
    public interface IComponent : Core.IDrawable, IBehavior, IDisposable
    {
        bool IsEnabled { get; set; }

        Transform LocalTransform { get; set; }

        Transform WorldTransform { get; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }

        Rectangle GetBounds();

        void UpdateWorldTransform(Transform parentTransform);

        void UpdateLocalTransform(Vector2? position = null, float? rotation = null, float? scale = null);
    }
}
