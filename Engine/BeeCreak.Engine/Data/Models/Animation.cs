namespace BeeCreak.Engine.Data.Models
{
    public record Animation
    (
        string Id,
        SpriteSheet SpriteSheet,
        List<string> Data
    );
}