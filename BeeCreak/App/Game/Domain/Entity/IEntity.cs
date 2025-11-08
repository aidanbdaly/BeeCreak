using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game.Domain.Entity
{
    public interface IEntity
    {
        EntityReference Reference { get; }
    }
}
