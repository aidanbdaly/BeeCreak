using Microsoft.Xna.Framework;

public class EntityStateDTO
{
    public string PersistentId { get; set; } = string.Empty;

    public Vector2 Position { get; set; }

    public float Velocity { get; set; }

    public string Sprite { get; set; } = string.Empty;

    public Rectangle HitBox { get; set; }

    public bool Controlled { get; set; }
}