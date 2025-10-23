using BeeCreak.src.Models;

namespace BeeCreak
{
    public interface IEntity
    {
        EntityState State { get; }

        EntityAttributes Attributes { get; }
    }
}