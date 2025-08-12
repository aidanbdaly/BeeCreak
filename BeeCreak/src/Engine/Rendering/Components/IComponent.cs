using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public interface IComponent : IDrawable
    {
        bool IsEnabled { get; set; }

        Vector2 Position { get; set; }

        float Scale { get; set; }

        Color Color { get; set; }

        float Rotation { get; set; }

        Vector2 Origin { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }

        Rectangle GetBounds();

        Rectangle GetTextureBounds();
        
        void Import(IComponent component);
    }
}
