using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.Scene.Tile.Instances;

public class GrassTile : Tile
{
    public GrassTile(IToolCollection tools)
        : base(tools)
    {
        Texture = tools.Static.Sprite.GetTexture("grass");
        Type = TileType.Grass;
    }

    public GrassTile(IToolCollection tools, Vector2 position, Rectangle bounds = default)
        : base(tools, position, bounds)
    {
        Texture = tools.Static.Sprite.GetTexture("grass");
        Type = TileType.Grass;
    }

    public override GrassTileDTO ToDTO()
    {
        return new GrassTileDTO
        {
            Position = Position,
            Type = Type,
            Bounds = Bounds
        };
    }
}
