using Microsoft.Xna.Framework;
using BeeCreak.Extensions.EntityRecord;

namespace BeeCreak.Extensions.EntityReference;

public sealed class EntityReferenceContent
{
    public string Id { get; set; }

    public EntityRecordContent BaseEntity { get; set; }

    public string Variant { get; set; }

    public Vector2 Position { get; set; }
}
