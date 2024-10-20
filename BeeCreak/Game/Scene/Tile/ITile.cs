namespace BeeCreak.Game.Scene.Tile
{
    using Microsoft.Xna.Framework;

    public interface ITile
    {
        Vector2 Position { get; set; }

        Rectangle Bounds { get; set; }

        void Draw();

        TileDTO ToDTO();
    }
}
