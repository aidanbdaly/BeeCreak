using BeeCreak.App.Game.Domain.Entity;

namespace BeeCreak.App.Game.Models
{
    public record EntityReference(
        string Id,
        EntityRecord Base,
        CellReference Cell,
        EntityState State
    );
}
