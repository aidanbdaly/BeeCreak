using BeeCreak.Engine.Data.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record Entity
    (   string Id,
        List<Animation> Animation,
        BoundingBoxSheet BoundingBoxSheet,
        List<EntityBehaviour> Behaviours
    );
}
