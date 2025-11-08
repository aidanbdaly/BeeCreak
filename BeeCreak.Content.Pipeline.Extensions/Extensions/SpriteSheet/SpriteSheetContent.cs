using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

public class SpriteSheetContent
{
    public string Id { get; set; }

    public Texture2DContent Image { get; set; }

    public Dictionary<string, Rectangle> Sprites { get; set; } = new();
}
