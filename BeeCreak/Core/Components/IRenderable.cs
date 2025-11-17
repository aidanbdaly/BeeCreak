using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public interface IRenderable 
    {
        State<Vector2> Position { get; set; }

        float Rotation { get; set; }

        Vector2 Scale { get; set; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        SpriteEffects Effects { get; set; }

        float LayerDepth { get; set; }

        Rectangle GetBounds();

        void Draw(SpriteBatch spriteBatch);
    }
}
