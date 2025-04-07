using Microsoft.Xna.Framework;

public class TileFactory
{
    public Tile fromDTO(TileDTO tileDTO)
    {
        return new Tile()
        {
            Type = tileDTO.Type,
            Variant = tileDTO.Variant,
            Position = new Vector2(tileDTO.Position.X, tileDTO.Position.Y)
        };
    }

    public TileDTO toDTO(Tile tile)
    {
        return new TileDTO()
        {
            Type = tile.Type,
            Variant = tile.Variant,
            Position = new Vector2DTO()
            {
                X = tile.Position.X,
                Y = tile.Position.Y
            }
        };
    }
}