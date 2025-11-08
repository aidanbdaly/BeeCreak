using System.Collections.Generic;

namespace BeeCreak.Content.Pipeline.Extensions.EntityRecord;

public sealed class EntityRecordDto
{
    public string Id { get; set; }

    public string SpriteSheet { get; set; }

    public List<string> Behaviours { get; set; } = [];
}
