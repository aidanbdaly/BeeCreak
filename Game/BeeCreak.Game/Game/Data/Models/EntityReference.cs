using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record EntityReference(
        string Id,
        Entity Base,
        EntityState State
    );
}
