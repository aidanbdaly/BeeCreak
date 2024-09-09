using System;
using System.Collections.Generic;
public class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Action<object>>> Subscribers = new();

    public void Subscribe<T>(Action<T> action)
    {
        if (!Subscribers.ContainsKey(typeof(T)))
        {
            Subscribers[typeof(T)] = new List<Action<object>>();
        }

        Subscribers[typeof(T)].Add(obj => action((T)obj));

        // Amazing!!
    }

    public void Publish<T>(T obj)
    {
        if (Subscribers.ContainsKey(typeof(T)))
        {
            foreach (var handler in Subscribers[typeof(T)])
            {
                handler(obj);
            }
        }
    }
}
