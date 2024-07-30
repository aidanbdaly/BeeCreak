using System;
using BeeCreak.Run.Generation;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

public class Circle : RouterCommand
{
    public override bool IsSymmetric { get; } = true;
    public override Shape Shape { get; }
    public override int Offset { get; }
    private int Radius { get; set; }
    private Random Random { get; set; } = new Random();
    public Circle(int radius)
    {
        Shape = Shape.Circle(radius);
        Radius = radius;

        Offset = radius;
    }

    public override RouterBit MoveRouterBit(RouterBit routerBit)
    {
        // In here is moving AND turning logic, do we need to separate them?

        var newDirection = routerBit.Direction;

        var circleOffsetX = Radius * routerBit.Direction.Value.X;
        var circleOffsetY = Radius * routerBit.Direction.Value.Y;

        switch (Random.Next(0, 3))
        {
            case 0:
            {
                newDirection = Direction.Next(newDirection);
                break;
            }
            case 1:
            {
                newDirection = Direction.Prev(newDirection);
                break;
            }
            case 2:
            {
                break;
            }
        }

        routerBit.Position += new Vector2(
            circleOffsetX + (Radius + 1) * newDirection.Value.X,
            circleOffsetY + (Radius + 1) * newDirection.Value.Y
        );

        routerBit.Direction = newDirection;

        return routerBit;
    }
}
