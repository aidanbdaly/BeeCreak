using System;

namespace BeeCreak.Tools.Static;
public interface IEventManager
{
    void Listen<T>(Action<T> action, string channel = null);
    void Dispatch<T>(T obj, string channel = null);
}
