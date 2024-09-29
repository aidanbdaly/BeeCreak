using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Game.Objects.Time;

public interface ITime : IDynamic
{
    int Current { get; set; }

    TimeDTO ToDTO();
}
