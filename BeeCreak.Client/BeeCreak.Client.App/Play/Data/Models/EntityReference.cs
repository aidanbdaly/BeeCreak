using BeeCreak.Domain.Entity;

namespace BeeCreak.Models
{
    public record EntityReference(
        string Id,
        Entity Base,
        EntityState State
    );
}
