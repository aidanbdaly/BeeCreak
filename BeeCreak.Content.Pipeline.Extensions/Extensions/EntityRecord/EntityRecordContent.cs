using System.Collections.Generic;
using BeeCreak.Content.Pipeline.Extensions.AnimationSheet;

namespace BeeCreak.Content.Pipeline.Extensions.EntityRecord;

public sealed class EntityRecordContent
{
    public string Id { get; set; }

    public AnimationSheetContent AnimationSheet { get; set; }

    public List<string> Behaviours { get; } = [];
}
