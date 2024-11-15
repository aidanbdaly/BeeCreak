namespace BeeCreak.Game.Scene.Tile
{
    using System;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Static;

    public class TileMapFactory
    {
        private readonly ISprite sprite;

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
                    };

                    tileDTOArray[x, y] = tileDTO;
                }
            }

            return new TileMapDTO(tileDTOArray);
        }

        public TileMap CreateTileMap(TileMapDTO tileMapDTO)
        {
            var tiles = tileMapDTO.Tiles;

            var size = tiles.GetLength(0);

            var tileArray = new Tile[size, size];

            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var tileDTO = tiles[x, y];

                    var tile = new Tile(sprite, tileDTO.Type);

                    tileArray[x, y] = tile;
                }
            }

            return new TileMap(tileArray);
        }
    }
}