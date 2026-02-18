using BeeCreak.Engine.Data.Models;
using BeeCreak.Domain.Entity;

namespace BeeCreak.Models
{
    public enum EntityBehaviour
    {
        
    }
    public record Entity(
        string Id,
        AnimationCollection AnimationCollection,
        BoundingBoxSheet BoundingBoxSheet,
        List<EntityBehaviour> Behaviours);
}
