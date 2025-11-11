using Microsoft.Xna.Framework;
using BeeCreak.Content.Pipeline.Extensions.EntityRecord;

namespace BeeCreak.Content.Pipeline.Extensions.EntityReference;

public sealed class EntityReferenceContent
{
    public string Id { get; set; }

    public EntityRecordContent BaseEntity { get; set; }

    public string Variant { get; set; }

    public Vector2 Position { get; set; }
}
