using System.Collections.Generic;

namespace BeeCreak.Extension.Generated;

public sealed class EntityContent
{
public string Id { get; set; }

public AnimationCollectionContent AnimationCollection { get; set; }

public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

public List<string> Behaviours { get; } = new();

}
