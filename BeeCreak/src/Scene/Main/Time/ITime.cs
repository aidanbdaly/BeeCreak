using BeeCreak.Shared;

namespace BeeCreak.Scene.Main;

public interface ITime : IDynamic
{
    int Current { get; set; }
}
