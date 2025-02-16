namespace BeeCreak.Shared.Data.Models;

public class ButtonAssetDTO
{
    public ButtonAssetDTO()
    {
    }

    public RectangleDTO SourceRectangle { get; set; } = new();

    public RectangleDTO BoundingBox { get; set; } = new();
}