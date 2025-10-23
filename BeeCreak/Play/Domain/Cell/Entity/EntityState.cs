using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class EntityState
    {    
        public string ContentId { get; set; } = string.Empty;
    
        public string Variant { get; set; } = "facing_west";
    
        public Vector2 Position { get; set; }
    }
}
