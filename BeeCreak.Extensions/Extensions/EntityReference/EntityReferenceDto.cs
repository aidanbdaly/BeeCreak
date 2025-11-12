namespace BeeCreak.Extensions.EntityReference;

public sealed class EntityReferenceDto
{
    public string Id { get; set; }

    public string Base { get; set; }

    public string Cell { get; set; }

    public string Variant { get; set; }

    public EntityReferencePositionDto Position { get; set; }
}

public sealed class EntityReferencePositionDto
{
    public float X { get; set; }

    public float Y { get; set; }
}
