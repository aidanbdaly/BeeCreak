using BeeCreak.Core.Models;
using BeeCreak.Game.Domain.Entity;

namespace BeeCreak.Game.Models
{
    public record EntityReference(
        string Id,
        EntityModel Base,
        CellReference Cell,
        EntityState State
    );
}
