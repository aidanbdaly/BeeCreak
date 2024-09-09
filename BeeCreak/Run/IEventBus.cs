using System;
public interface IEventBus
{
    void Subscribe<T>(Action<T> action);
    void Publish<T>(T obj);
}
