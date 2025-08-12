using Microsoft.Xna.Framework;
using System.Collections.Generic;

public sealed class TileAttributesContent
{
    public Rectangle? HitBox { get; init; }
    public bool IsVariable { get; init; }
}

public sealed class TileCatalogueContent : Dictionary<string, TileAttributesContent> { }