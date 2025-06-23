using Microsoft.Xna.Framework;
using System.Collections.Generic;

public sealed class TileVariantContent
{
    public Rectangle? HitBox { get; init; }
}

public sealed class TileTypeContent
{
    public TileVariantContent Default  { get; init; } = new();

    public Dictionary<string, TileVariantContent> Variants { get; init; } = [];
}

public sealed class TileCatalogueContent : Dictionary<string, TileTypeContent> { }