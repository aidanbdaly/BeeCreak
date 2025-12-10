using BeeCreak.Engine.Data.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record Entity(
        string Id,
        AnimationCollection AnimationCollection,
        BoundingBoxSheet BoundingBoxSheet,
        List<EntityBehaviour> Behaviours);
}
