using System;
using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Game.Scene.Tile.Instances;

public class ForestTileDTO : TileDTO
{
    public ForestTileDTO() { }

    public override ForestTile FromDTO(IToolCollection tools)
    {
        return new ForestTile(tools, Position);
    }
}
