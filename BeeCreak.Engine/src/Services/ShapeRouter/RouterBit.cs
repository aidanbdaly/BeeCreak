using System;
using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Services.Static;

public class RouterBit
{
    public Vector2 Position { get; set; }

    public Direction Direction { get; set; }

    private ITile[,] Tiles { get; set; }

    public void SetTiles(ITile[,] tiles)
    {
        Tiles = tiles;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void SetDirection(Direction direction)
    {
        Direction = direction;
    }

    public void RotateLeft()
    {
        Direction = Direction.Prev(Direction);
    }

    public void RotateRight()
    {
        Direction = Direction.Next(Direction);
    }

    public bool Execute(RouterCommand command)
    {
        var shapeCoordinates = command.Shape.Coordinates;

        if (!command.IsSymmetric)
        {
            shapeCoordinates = Shape.Rotate(shapeCoordinates, Direction);
        }

        foreach (var (x, y) in shapeCoordinates)
        {
            var X = x + (int)Position.X + (command.Offset * (int)Direction.Value.X);
            var Y = y + (int)Position.Y + (command.Offset * (int)Direction.Value.Y);

            if (!IsInWorld(X, Y, Tiles.GetLength(0)) || Tiles[X, Y] != null)
            {
                return false;
            }
        }

        foreach (var (x, y) in shapeCoordinates)
        {
            var X = x + (int)Position.X + (command.Offset * (int)Direction.Value.X);
            var Y = y + (int)Position.Y + (command.Offset * (int)Direction.Value.Y);

            var position = new Vector2(X * GlobalConstants.TILE_RESOLUTION, Y * GlobalConstants.TILE_RESOLUTION);
            
            var tile = new Tile()
            {
                Type = TileAssetType.GrassOnWater,
                Variant = TileAssetVariant.Default,
                Position = position
            };

            Tiles[X, Y] = tile;
        }

        var translatedBit = command.MoveRouterBit(this);

        Position = translatedBit.Position;
        Direction = translatedBit.Direction;

        return true;
    }

    private static bool IsInWorld(int x, int y, int size)
    {
        return x >= 0 && y >= 0 && x < size && y < size;
    }
}