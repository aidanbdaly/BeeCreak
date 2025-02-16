using System.Drawing;

namespace BeeCreak.Shared.Data.Models;

public class ButtonAsset
{
    public ButtonAsset()
    {
    }

    public Rectangle SourceRectangle { get; set; } = new();

    public Rectangle BoundingBox { get; set; } = new();
}