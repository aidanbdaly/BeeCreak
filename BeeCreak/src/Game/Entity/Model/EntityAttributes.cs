using Microsoft.Xna.Framework;

public class EntityAttributes
{
    public float BaseVelocity { get; set; } = 1f;

    public bool Controlled { get; set; } = false;

    public Rectangle? HitBox { get; set; }
}