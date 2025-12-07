using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class EntityModelContent
{
public string Id { get; set; }

public List<AnimationContent> Animations { get; } = new();

public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

public List<string> Behaviours { get; } = new();

}
