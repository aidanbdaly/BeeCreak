namespace BeeCreak.Generation
{
    using Microsoft.Xna.Framework;

    public class Line : RouterCommand
    {
        public Line(int length, int width)
        {
            Shape = Shape.Line(length, width);
            Length = length;

            Offset = length / 2;
        }

        public override bool IsSymmetric { get; } = false;

        public override Shape Shape { get; }

        public override int Offset { get; }

        private int Length { get; set; }

        public override RouterBit MoveRouterBit(RouterBit routerBit)
        {
            var newDirection = routerBit.Direction;

            var (xFactor, yFactor) = newDirection.Value;

            routerBit.Position += new Vector2(Length * xFactor, Length * yFactor);

            return routerBit;
        }
    }
}
