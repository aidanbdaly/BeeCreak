using System.Collections.Generic;
using BeeCreak.Extensions.Animation;
using BeeCreak.Extensions.BoundingBoxSheet;

namespace BeeCreak.Extensions.Entity.Model;

public sealed class EntityModelContent
{
    public string Id { get; set; }

    public List<AnimationContent> Animations { get; set; }

    public BoundingBoxSheetContent BoundingBoxSheet { get; set; }

    public List<string> Behaviours { get; } = [];
}
