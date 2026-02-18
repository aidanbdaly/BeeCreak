namespace BeeCreak.Engine.Services.Layout
{
    public readonly struct LayoutValue
    {
        public LayoutValue(LayoutUnit unit, float value)
        {
            Unit = unit;
            Value = value;
        }

        public LayoutUnit Unit { get; }

        public float Value { get; }

        public bool IsAuto => Unit == LayoutUnit.Auto;

        public static LayoutValue Auto => new(LayoutUnit.Auto, 0f);

        public static LayoutValue Px(float pixels) => new(LayoutUnit.Pixel, pixels);

        public static LayoutValue Percent(float percent)
        {
            var normalized = percent > 1f ? percent / 100f : percent;
            return new LayoutValue(LayoutUnit.Percent, normalized);
        }

        public float Resolve(float available)
        {
            return Unit switch
            {
                LayoutUnit.Pixel => Value,
                LayoutUnit.Percent => available * Value,
                _ => float.NaN
            };
        }

        public override string ToString()
        {
            return Unit switch
            {
                LayoutUnit.Pixel => $"{Value}px",
                LayoutUnit.Percent => $"{Value * 100f}%",
                _ => "auto"
            };
        }
    }
}
