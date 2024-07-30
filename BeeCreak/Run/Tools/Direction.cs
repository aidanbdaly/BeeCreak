using System;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Tools;

public enum DirectionType
{
    Up,
    Down,
    Left,
    Right
}

public struct Direction
{
    public Vector2 Value { get; set; }
    public DirectionType Type { get; set; }

    private Direction(Vector2 value)
    {
        Value = value;
        Type = value switch
        {
            (0, 1) => DirectionType.Up,
            (0, -1) => DirectionType.Down,
            (-1, 0) => DirectionType.Left,
            (1, 0) => DirectionType.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static Direction Up => new(Vector2.UnitY);
    public static Direction Down => new(-Vector2.UnitY);
    public static Direction Left => new(-Vector2.UnitX);
    public static Direction Right => new(Vector2.UnitX);

    public static Direction Next(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.Up => Right,
            DirectionType.Right => Down,
            DirectionType.Down => Left,
            DirectionType.Left => Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Direction Prev(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.Up => Left,
            DirectionType.Left => Down,
            DirectionType.Down => Right,
            DirectionType.Right => Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
