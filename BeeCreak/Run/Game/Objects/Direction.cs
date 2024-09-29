using System;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Tools;

public enum DirectionType
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
}

public struct Direction
{
    public Vector2 Value { get; set; } = Vector2.Zero;
    public DirectionType Type { get; set; }

    private Direction(Vector2 value)
    {
        Value = value;
        Type = value switch
        {
            (0, -1) => DirectionType.North,
            (1, -1) => DirectionType.NorthEast,
            (1, 0) => DirectionType.East,
            (1, 1) => DirectionType.SouthEast,
            (0, 1) => DirectionType.South,
            (-1, 1) => DirectionType.SouthWest,
            (-1, 0) => DirectionType.West,
            (-1, -1) => DirectionType.NorthWest,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static Direction North => new(-Vector2.UnitY);
    public static Direction NorthEast => new(-Vector2.UnitY + Vector2.UnitX);
    public static Direction East => new(Vector2.UnitX);
    public static Direction SouthEast => new(Vector2.UnitY + Vector2.UnitX);
    public static Direction South => new(Vector2.UnitY);
    public static Direction SouthWest => new(Vector2.UnitY - Vector2.UnitX);
    public static Direction West => new(-Vector2.UnitX);
    public static Direction NorthWest => new(-Vector2.UnitY - Vector2.UnitX);

    public static Direction Next(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.North => East,
            DirectionType.East => South,
            DirectionType.South => West,
            DirectionType.West => North,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static Direction Prev(Direction direction)
    {
        return direction.Type switch
        {
            DirectionType.North => West,
            DirectionType.West => South,
            DirectionType.South => East,
            DirectionType.East => North,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
