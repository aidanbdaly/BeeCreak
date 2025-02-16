using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class TileDTO
{
    public TileType Type { get; set; } = TileType.Default;

    public TileVariant Variant { get; set; } = TileVariant.Default;

    public Vector2 Position { get; set; } = new();
}