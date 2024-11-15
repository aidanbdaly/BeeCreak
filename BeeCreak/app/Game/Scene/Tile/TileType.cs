namespace BeeCreak.Game.Scene.Tile;

using System.Collections.Generic;
public enum TileType
{
    Forest,
    Grass,
}

public static class TileDictionary
{
    public static Dictionary<TileType, string> Textures = new ()
    {
        { TileType.Forest, "forest" },
        { TileType.Grass, "grass" },
    };
}