using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

public sealed class AnimationContent
{
public string Id { get; set; }

public TextureContent Texture { get; set; }

public List<DataEntryContent> Data { get; } = new();

public sealed class DataEntryContent
    {
public int X { get; set; }

public int Y { get; set; }

public int W { get; set; }

public int H { get; set; }

}

}
