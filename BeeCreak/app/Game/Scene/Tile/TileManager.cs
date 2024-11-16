namespace BeeCreak.Game.Scene.Tile
{
    using global::BeeCreak.Config;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TileManager
    {
        private readonly ISprite sprite;

        public TileManager(ISprite sprite)
        {
            this.sprite = sprite;
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
