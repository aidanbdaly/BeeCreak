using System.Collections.Generic;
using Microsoft.Xna.Framework;
internal class SpriteSheetDTO
{
    public string ImageName { get; set; }
    
    public Dictionary<string, Rectangle> Frames { get; set; } = new();
}