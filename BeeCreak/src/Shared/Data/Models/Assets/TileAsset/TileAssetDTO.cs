using BeeCreak.Shared.UI;

namespace BeeCreak.Shared.Data.Models;

public class TileAssetDTO
{
    public TileAssetDTO()
    {
    }

    public RectangleDTO SourceRectangle { get; set; } = new();

    public RectangleDTO BoundingBox { get; set; } = new();
}
