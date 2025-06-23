using System;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

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
    private Direction(Vector2 value, DirectionType type)
    {
        Value = value;
        Type = type;
    }

    public static Direction North => new(-Vector2.UnitY, DirectionType.North);

    public static Direction NorthEast => new(-Vector2.UnitY + Vector2.UnitX, DirectionType.NorthEast);

    public static Direction East => new(Vector2.UnitX, DirectionType.East);

    public static Direction SouthEast => new(Vector2.UnitY + Vector2.UnitX, DirectionType.SouthEast);

    public static Direction South => new(Vector2.UnitY, DirectionType.South);

    public static Direction SouthWest => new(Vector2.UnitY - Vector2.UnitX, DirectionType.SouthWest);

    public static Direction West => new(-Vector2.UnitX, DirectionType.West);

    public static Direction NorthWest => new(-Vector2.UnitY - Vector2.UnitX, DirectionType.NorthWest);

    public static List<Direction> CardinalDirections => new()
    {
        North,
        East,
        South,
        West
    };

    public Vector2 Value { get; set; }

    public DirectionType Type { get; set; }

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
