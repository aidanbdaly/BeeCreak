using BeeCreak.Shared.UI;

namespace BeeCreak.Shared.Data.Models;

public class SpriteDTO
{
    public string Name { get; set; }

    public RectangleDTO SourceRectangle { get; set; } = new();
}