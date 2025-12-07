namespace BeeCreak.Extension.Generated;

public sealed class EntityReferenceContent
{
public string Id { get; set; }

public EntityModelContent EntityModel { get; set; }

public string Variant { get; set; }

public PositionContent Position { get; set; } = new();

    public sealed class PositionContent
    {
public double X { get; set; }

public double Y { get; set; }

}

}
