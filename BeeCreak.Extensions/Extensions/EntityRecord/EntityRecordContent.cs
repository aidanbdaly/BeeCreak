using System.Collections.Generic;
using BeeCreak.Extensions.AnimationSheet;
using BeeCreak.Extensions.BoundingBoxSheet;

namespace BeeCreak.Extensions.EntityRecord;

public sealed class EntityRecordContent
{
    public string Id { get; set; }

    public AnimationSheetContent AnimationSheet { get; set; }

    public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

    public List<string> Behaviours { get; } = [];
}
