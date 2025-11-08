using System.Collections.Generic;
using BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

namespace BeeCreak.Content.Pipeline.Extensions.EntityRecord;

public sealed class EntityRecordContent
{
    public string Id { get; set; }

    public SpriteSheetContent SpriteSheet { get; set; }

    public List<string> Behaviours { get; } = [];
}
