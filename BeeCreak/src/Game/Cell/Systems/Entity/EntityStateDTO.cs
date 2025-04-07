using System.Numerics;

public class EntityStateDTO
{
    public string PersistentId { get; set; }

    public Vector2DTO Position { get; set; }

    public float Velocity { get; set; }

    public string Sprite { get; set; }
}