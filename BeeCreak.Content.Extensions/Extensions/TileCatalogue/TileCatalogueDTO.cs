using Microsoft.Xna.Framework;
using System.Collections.Generic;

public sealed class TileAttributesDto
{
    /// <summary> Hit-box for this tile type. </summary>
    public Rectangle? HitBox { get; init; }
    
    /// <summary> Whether this tile has variants. </summary>
    public bool IsVariable { get; init; }
}

public sealed class TileCatalogueDto : Dictionary<string, TileAttributesDto> { }