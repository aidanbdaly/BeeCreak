namespace BeeCreak.Engine.Services.Layout
{
    public sealed class LayoutStyle
    {
        public LayoutValue Width { get; set; } = LayoutValue.Auto;

        public LayoutValue Height { get; set; } = LayoutValue.Auto;

        public LayoutBox Margin { get; set; } = LayoutBox.Empty;

        public LayoutBox Padding { get; set; } = LayoutBox.Empty;

        public LayoutDirection Direction { get; set; } = LayoutDirection.Column;

        public LayoutAlign Align { get; set; } = LayoutAlign.Start;

        public LayoutJustify Justify { get; set; } = LayoutJustify.Start;

        public LayoutPosition Position { get; set; } = LayoutPosition.Relative;

        public LayoutValue? Left { get; set; }

        public LayoutValue? Top { get; set; }

        public LayoutValue? Right { get; set; }

        public LayoutValue? Bottom { get; set; }

        public float Gap { get; set; }
    }
}
