using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class EntityDto
{
public string Id { get; set; }

public List<string> Animations { get; set; } = new();

public string BoundingBoxSheet { get; set; }

public List<string> Behaviours { get; set; } = new();

}
