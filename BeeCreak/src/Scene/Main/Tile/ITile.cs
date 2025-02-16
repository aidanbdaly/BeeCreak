using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface ITile
{
    TileType Type { get; set; }

    TileVariant Variant { get; set; }

    Rectangle SourceRectangle { get; set; }

    Rectangle Bounds { get; set; }

    Vector2 Position { get; set; }

    void SetType(TileType type);

    void SetVariant(TileVariant variant);

    void SetPosition(Vector2 position);

    void Draw();
}

