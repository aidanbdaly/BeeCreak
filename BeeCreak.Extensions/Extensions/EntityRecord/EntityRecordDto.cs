using System.Collections.Generic;

namespace BeeCreak.Extensions.EntityRecord;

public sealed class EntityRecordDto
{
    public string Id { get; set; }

    public string AnimationSheet { get; set; }

    public string BoundingBoxSheet { get; set; }

    public List<string> Behaviours { get; set; } = [];
}
