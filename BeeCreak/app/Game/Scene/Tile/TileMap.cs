namespace BeeCreak.Game.Scene.Tile
{
    public class TileMap : ITileMap
    {
        public TileMap(ITile[,] tiles)
        {
            Tiles = tiles;
        }

        public ITile[,] Tiles { get; set; }

        public ITile GetTile(int x, int y)
        {
            return Tiles[x, y];
        }
    }
}