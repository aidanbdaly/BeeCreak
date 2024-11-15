namespace BeeCreak.Game.Scene.Tile
{
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Tile : ITile
    {
        public Tile(ISprite sprite, TileType type)
        {
            Texture = sprite.GetTexture(TileDictionary.Textures[type]);
            Type = type;
        }

        public TileType Type { get; set; }

        public Rectangle Bounds { get; set; }

        public Texture2D Texture { get; set; }
    }
}