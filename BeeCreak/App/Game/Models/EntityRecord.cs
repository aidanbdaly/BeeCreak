using System.Collections.Immutable;
using BeeCreak.Core.Models;
using BeeCreak.App.Game.Domain.Entity;

namespace BeeCreak.App.Game.Models
{
    public record EntityRecord
    (   string Id,
        AnimationSheet AnimationSheet,
        BoundingBoxSheet BoundingBoxSheet,
        ImmutableList<EntityBehaviour> Behaviours
    );
}
