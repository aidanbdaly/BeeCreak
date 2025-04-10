namespace BeeCreak.Shared.Data.Models;

public class AnimationDTO
{
    public string? SpriteSheetName { get; set; }
    
    public int TimePerFrame { get; set; }

    public bool Loop { get; set; }

    public List<Frame> Frames { get; set; } = new();
}