using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Services.Layout
{
    public class UIComponent
    {
        public Texture2D? Texture { get; set; } = null;

        public SpriteFont? Font { get; set; } = null;

        public string? Text { get; set; } = null;

        public Rectangle DestinationRectangle { get; set; } = Rectangle.Empty;

        public Rectangle SourceRectangle { get; set; } = Rectangle.Empty;

        public Vector2 Position { get; set; } = Vector2.Zero;

        public Color Color { get; set; } = Color.White;

        public float Opacity { get; set; } = 1f;

        public float Rotation { get; set; } = 0f;

        public Vector2 Origin { get; set; } = default;

        public Vector2 Scale { get; set; } = Vector2.One;

        public SpriteEffects Effect { get; set; } = default;

        public float LayerDepth { get; set; } = 0f;
    }
}