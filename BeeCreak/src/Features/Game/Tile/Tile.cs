namespace BeeCreak.Game.Scene.Tile
{
    using Microsoft.Xna.Framework;

    public class Tile : ITile
    {
        public Tile(TileType type, TileVariant variant, Vector2 position, Rectangle bounds = default)
        {
            Type = type;
            Variant = variant;
            Position = position;
            Bounds = bounds;
        }

        public TileType Type { get; set; }

        public TileVariant Variant { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Bounds { get; set; }


        public void SetVariant(TileVariant variant)
        {
            Variant = variant;
        }

        public void SetType(TileType type)
        {
            Type = type;
        }
    }
}