namespace BeeCreak.Game.Scene.Tile
{
    public class TileManager
    {
        public TileManager()
        {
        }

        public ITileMap TileMap { get; set; }

        public void SetTileMap(ITileMap tileMap)
        {
            TileMap = tileMap;
        }

        public void Draw()
        {
            TileMap.Draw();
        }
    }
}
