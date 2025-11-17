using BeeCreak.Core.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public abstract class Renderable(
        Vector2 position = default,
        Color color = default,
        float rotation = 0f,
        Vector2 origin = default,
        Vector2 scale = default,
        SpriteEffects effects = default,
        float layerDepth = 0f
    ) : IRenderable
    {
        public State<Vector2> Position { get; set; } = new(position);

        public Color Color { get; set; } = color;

        public float Rotation { get; set; } = rotation;

        public Vector2 Origin { get; set; } = origin;

        public Vector2 Scale { get; set; } = scale;

        public SpriteEffects Effects { get; set; } = effects;

        public float LayerDepth { get; set; } = layerDepth;

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract Rectangle GetBounds();
    }
}
