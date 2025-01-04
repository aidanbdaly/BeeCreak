using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Scene.Tile;

public interface ITile
{
    TileType Type { get; set; }

    TileVariant Variant { get; set; }

    Rectangle Bounds { get; set; }

    Vector2 Position { get; set; }

    void SetType(TileType type);

    void SetVariant(TileVariant variant);
}

