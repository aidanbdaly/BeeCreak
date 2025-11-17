using BeeCreak.Core.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record EntityReference(
        string Id,
        EntityRecord Base,
        CellReference Cell,
        EntityState State
    );
}
