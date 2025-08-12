using Microsoft.Xna.Framework;

namespace BeeCreak
{
    
    
    public class EntityState
    {
        public string ContentId { get; init; }
    
        public string Variant { get; set; } = "facing_west";
    
        public Vector2 Position { get; set; }
    }
}
