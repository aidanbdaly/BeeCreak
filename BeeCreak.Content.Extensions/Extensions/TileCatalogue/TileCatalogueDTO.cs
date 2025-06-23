using Microsoft.Xna.Framework;
using System.Collections.Generic;

public sealed class TileVariantDto
{
    /// <summary> Optional override hit-box for this variant. </summary>
    public Rectangle? HitBox { get; init; }
}

public sealed class TileTypeDto
{
    /// <summary> Default that applies when there are no variants. </summary>
    public TileVariantDto Default  { get; init; } = new();

    /// <summary>
    /// Mapping from variant name (e.g. "Left", "Right") to its overrides.
    /// Empty object {} is allowed when the variant has no custom data.
    /// </summary>
    public Dictionary<string, TileVariantDto> Variants { get; init; } = [];
}

public sealed class TileCatalogueDto : Dictionary<string, TileTypeDto> { }