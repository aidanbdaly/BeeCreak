using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public interface IRenderable
    {
        State<Vector2> Position { get; set; }

        State<float> Rotation { get; set; }

        State<Vector2> Scale { get; set; }

        State<Color> Color { get; set; }

        State<Vector2> Origin { get; set; }

        State<SpriteEffects> Effects { get; set; }

        State<float> LayerDepth { get; set; }

        Rectangle Bounds { get; }

        void Draw(SpriteBatch spriteBatch);
    }
}
