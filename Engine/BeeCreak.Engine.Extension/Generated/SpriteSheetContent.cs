using System.Collections.Generic;

using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

public sealed class SpriteSheetContent
{
public string Id { get; set; }

public TextureContent Texture { get; set; }

public Dictionary<string, object> Data { get; } = new();

}
