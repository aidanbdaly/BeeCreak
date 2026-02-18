namespace BeeCreak.Engine.Services.Layout
{
    public readonly struct LayoutBox
    {
        public LayoutBox(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public float Left { get; }

        public float Top { get; }

        public float Right { get; }

        public float Bottom { get; }

        public float Horizontal => Left + Right;

        public float Vertical => Top + Bottom;

        public static LayoutBox Empty => new(0f, 0f, 0f, 0f);

        public static LayoutBox All(float value) => new(value, value, value, value);

        public static LayoutBox Symmetric(float horizontal, float vertical) =>
            new(horizontal, vertical, horizontal, vertical);
    }
}
