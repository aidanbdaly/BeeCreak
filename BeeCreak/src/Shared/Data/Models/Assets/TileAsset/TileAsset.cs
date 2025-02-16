using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Data.Models;

public class TileAsset
{
    public TileAsset()
    {
    }
    
    public Rectangle SourceRectangle { get; set; } = new();

    public Rectangle BoundingBox { get; set; } = new();
}