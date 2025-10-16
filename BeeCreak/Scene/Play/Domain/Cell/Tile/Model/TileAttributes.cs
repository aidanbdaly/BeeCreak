using Microsoft.Xna.Framework;

namespace BeeCreak
{
    
    public class TileAttributes
    {
        public Rectangle HitBox { get; set; } = Rectangle.Empty;
    
        public bool IsVariable { get; set; } = false;
    }
}
