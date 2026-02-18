namespace BeeCreak.Engine.Data.Models
{
    public record AnimationCollection
    (
        string Id,
        Dictionary<string, Animation> Data
    );
}