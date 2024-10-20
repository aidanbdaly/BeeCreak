using BeeCreak.Tools;

namespace BeeCreak.Game.Scene.Tile.Instances;

public class GrassTileDTO : TileDTO
{
    public GrassTileDTO() { }

    public override Tile FromDTO(IToolCollection tools)
    {
        return new GrassTile(tools, Position, Bounds);
    }
}
