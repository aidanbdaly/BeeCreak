using System.Collections.Generic;
using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

internal class SpriteSheetDTO
{
    public ExternalReference<Texture2D> SpriteSheetName { get; set; }
    
    public Dictionary<string, Frame> Frames { get; set; } = new();
}