using Microsoft.Xna.Framework;

namespace BeeCreak.Content.Pipeline.Extensions.EntityReference;

public sealed class EntityReferenceContent
{
    public string Id { get; set; }

    public string BaseId { get; set; }

    public string CellId { get; set; }

    public string Variant { get; set; }

    public Vector2 Position { get; set; }
}
