using System.Collections.Generic;
using Microsoft.Xna.Framework;

public sealed class EntityAttributesDTO
{
    public float BaseVelocity { get; init; } = 1f;

    public bool Controlled { get; init; } = false;

    public Rectangle? HitBox { get; init; }
}

public sealed class EntityTypeDTO
{
    /// <summary>
    /// Default variant that applies when there are no variants.
    /// </summary>
    public EntityAttributesDTO Default { get; init; } = new();

    /// <summary>
    /// Mapping from variant name (e.g. "Left", "Right") to its overrides.
    /// Empty object {} is allowed when the variant has no custom data.
    /// </summary>
    public Dictionary<string, EntityAttributesDTO> Variants { get; init; } = new();

}

public sealed class EntityCatalogueDTO : Dictionary<string, EntityTypeDTO> { }