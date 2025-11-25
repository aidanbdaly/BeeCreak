using System.Collections.Immutable;
using BeeCreak.Core.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record EntityModel
    (   string Id,
        Animation Animation,
        BoundingBoxSheet BoundingBoxSheet,
        ImmutableList<EntityBehaviour> Behaviours
    );
}
