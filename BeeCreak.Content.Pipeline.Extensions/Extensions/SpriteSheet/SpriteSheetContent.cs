using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Content.Pipeline.Extensions;

public class SpriteSheetContent
{
    public Texture2DContent Image { get; set; }

    public int Resolution { get; set; }

    public Dictionary<string, Rectangle> Frames { get; set; } = new();
}