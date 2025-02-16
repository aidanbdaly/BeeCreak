using BeeCreak.Scene.Main.Scene.Tile;

namespace BeeCreak.Shared.Data.Models;

public static class TileTypeExtensions
{
    public static bool IsVariable(this TileType type)
    {
        return type switch
        {
            TileType.GrassOnWater => true,
            _ => false,
        };
    }
}