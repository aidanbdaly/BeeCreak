namespace BeeCreak.Extension.Generated;

public sealed class EntityReferenceDto
{
public string Id { get; set; }

public string EntityModel { get; set; }

public string Variant { get; set; }

public PositionDto Position { get; set; }

    public sealed class PositionDto
    {
public double X { get; set; }

public double Y { get; set; }

}

}
