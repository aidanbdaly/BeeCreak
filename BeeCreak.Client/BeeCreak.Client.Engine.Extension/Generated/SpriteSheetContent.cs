using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

public sealed class SpriteSheetContent
{
public string Id { get; set; }

public TextureContent Texture { get; set; }

public Dictionary<string, DataEntryContent> Data { get; } = new();

public sealed class DataEntryContent
    {
public int X { get; set; }

public int Y { get; set; }

public int W { get; set; }

public int H { get; set; }

}

}
