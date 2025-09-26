using System;
using System.Collections.Generic;
using System.Linq;

public class EventBus : MonoSingleton<EventBus>
{
    private Dictionary<Type, List<Delegate>> _subscribers = new();

    public void Publish<T>(T evt)
    {
        if (_subscribers.TryGetValue(typeof(T), out var list))
        {
            foreach (var callback in list.Cast<Action<T>>())
                callback(evt);
        }
    }
    public void Subscribe<T>(Action<T> callback)
    {
        if (!_subscribers.TryGetValue(typeof(T), out var list))
        {
            list = new List<Delegate>();
            _subscribers[typeof(T)] = list;
        }
            
        list.Add(callback);
    }
    public void UnSubscribe<T>(Action<T> callback)
    {
        if (_subscribers.TryGetValue(typeof(T), out var list))
            list.Remove(callback);
    }

    public void Clear<T>()
    {
        _subscribers.Remove(typeof(T));
    }
    public void ClearAll()
    {
        _subscribers.Clear();
    }
}