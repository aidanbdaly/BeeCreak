namespace BeeCreak.Game.Scene.Tile;

using global::BeeCreak.Features.Game.Tile;
using global::BeeCreak.Tools.Static;

public class TileMapFactory
{
    private readonly ISprite sprite;

    private readonly TileAtlas tileAtlas;

    private readonly TileVariantCalculator tileVariator;

    public TileMapFactory(ISprite sprite)
    {
        this.sprite = sprite;
    }

    public static TileMapDTO CreateTileMapDTO(ITileMap tileMap)
    {
        var tiles = tileMap.Tiles;

        var size = tiles.GetLength(0);

        var tileDTOArray = new TileDTO[size, size];

        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                var tile = tiles[x, y];

                var tileDTO = new TileDTO()
                {
                    Type = tile.Type,
                    Variant = tile.Variant,
                    Position = tile.Position,
                    Bounds = tile.Bounds,
                };

                tileDTOArray[x, y] = tileDTO;
            }
        }

        return new TileMapDTO(tileDTOArray);
    }

    public TileMap CreateTileMap(TileMapDTO tileMapDTO)
    {
        var tileMap = new TileMap(sprite, tileAtlas, tileVariator);

        var tiles = tileMapDTO.Tiles;

        var size = tiles.GetLength(0);

        var tileArray = new Tile[size, size];

        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                var tileDTO = tiles[x, y];

                var tile = new Tile(tileDTO.Type, tileDTO.Variant, tileDTO.Position, tileDTO.Bounds);

                tileArray[x, y] = tile;
            }
        }

        tileMap.SetTiles(tileArray);

        return tileMap;
    }
}