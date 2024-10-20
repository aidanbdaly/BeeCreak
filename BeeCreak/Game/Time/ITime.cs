using BeeCreak.Tools;

namespace BeeCreak.Game.Objects.Time;

public interface ITime : IDynamic
{
    int Current { get; set; }

    TimeDTO ToDTO();
}
