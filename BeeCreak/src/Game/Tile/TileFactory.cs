using BeeCreak.Shared.Data.Config;
using Microsoft.Xna.Framework;

public class TileFactory
{
    public Tile fromDTO(TileDTO tileDTO)
    {
        return new Tile(
            tileDTO.Type,
            tileDTO.Variant,
            new Rectangle(
                tileDTO.Coordinate.X,
                tileDTO.Coordinate.Y,
                EngineConfig.TILE_RESOLUTION,
                EngineConfig.TILE_RESOLUTION
            ),
            tileDTO.Coordinate
        );
    }

    public TileDTO toDTO(Tile tile)
    {
        return new TileDTO()
        {
            Type = tile.Type,
            Variant = tile.Variant,
            Coordinate = tile.Coordinate
        };
    }
}