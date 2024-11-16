namespace BeeCreak.Game.Scene.Tile
{
    public interface ITileMap
    {
        ITile[,] Tiles { get; set; }

        ITile GetTile(int x, int y);

        void SetTiles(ITile[,] tiles);

        void Draw();
    }
}