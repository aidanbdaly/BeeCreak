namespace BeeCreak.Game.Scene.Tile
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface ITile
    {
        TileType Type { get; set; }

        Texture2D Texture { get; set; }

        Rectangle Bounds { get; set; }
    }
}
