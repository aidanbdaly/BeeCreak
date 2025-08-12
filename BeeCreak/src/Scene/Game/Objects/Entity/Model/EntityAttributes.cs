using Microsoft.Xna.Framework;

namespace BeeCreak.src.Models
{
    
    public class EntityAttributes
    {    
        public float BaseVelocity { get; set; } = 1f;
    
        public bool Controlled { get; set; } = false;
    
        public Rectangle HitBox { get; set; } = Rectangle.Empty;
    }
}
