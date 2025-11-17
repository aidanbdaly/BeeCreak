using System.Collections.Immutable;
using BeeCreak.Core.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record EntityRecord
    (   string Id,
        AnimationSheet AnimationSheet,
        BoundingBoxSheet BoundingBoxSheet,
        ImmutableList<EntityBehaviour> Behaviours
    );
}
