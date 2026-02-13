using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services.Layout
{
    public readonly struct LayoutRect
    {
        public LayoutRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float X { get; }

        public float Y { get; }

        public float Width { get; }

        public float Height { get; }

        public float Right => X + Width;

        public float Bottom => Y + Height;

        public Rectangle ToRectangle()
        {
            var width = (int)System.MathF.Round(Width);
            var height = (int)System.MathF.Round(Height);
            var x = (int)System.MathF.Round(X);
            var y = (int)System.MathF.Round(Y);
            if (width < 0)
            {
                width = 0;
            }

            if (height < 0)
            {
                height = 0;
            }

            return new Rectangle(x, y, width, height);
        }
    }
}
