using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class EntityDto
{
public string Id { get; set; }

public string AnimationCollection { get; set; }

public string BoundingBoxSheet { get; set; }

public List<string> Behaviours { get; set; } = new();

}
