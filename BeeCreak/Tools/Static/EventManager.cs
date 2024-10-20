using System;
using System.Collections.Generic;

namespace BeeCreak.Tools.Static;
public class EventManager : IEventManager
{
    private readonly Dictionary<Type, List<(Action<object>, string)>> Subscribers = new();

    public void Listen<T>(Action<T> action, string channel = null)
    {
        if (!Subscribers.ContainsKey(typeof(T)))
        {
            Subscribers[typeof(T)] = new List<(Action<object>, string)>();
        }

        Subscribers[typeof(T)].Add((obj => action((T)obj), channel));
    }

    public void Dispatch<T>(T obj, string channel = null)
    {
        if (Subscribers.ContainsKey(typeof(T)))
        {
            foreach (var handler in Subscribers[typeof(T)])
            {
                if (handler.Item2 == channel || channel == null)
                {
                    handler.Item1(obj);
                }
            }
        }
    }
}
