using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Content.Pipeline.Extensions;

public class SpriteSheetDTO
{
    public string ImageName { get; set; }
    
    public int Resolution { get; set; }
    
    public Dictionary<string, Rectangle> Frames { get; set; } = new();
}