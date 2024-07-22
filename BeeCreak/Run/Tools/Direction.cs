namespace BeeCreak.Run;

using System;

public enum DirectionType
{
    Up,
    Down,
    Left,
    Right
}

public struct Direction
{
    public (int, int) Value { get; set; }
    public DirectionType Type { get; set; }

    private Direction((int, int) value)
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

    public static Direction Up => new((0, 1));
    public static Direction Down => new((0, -1));
    public static Direction Left => new((-1, 0));
    public static Direction Right => new((1, 0));

    public static Direction Next(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.Up => Direction.Right,
            DirectionType.Right => Direction.Down,
            DirectionType.Down => Direction.Left,
            DirectionType.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Direction Prev(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.Up => Direction.Left,
            DirectionType.Left => Direction.Down,
            DirectionType.Down => Direction.Right,
            DirectionType.Right => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
