using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : class, new()
{
    private Queue<T> _instances = new();
    private Func<T> _factory;

    public ObjectPool(Func<T> factory = null, int preload = 0)
    {
        _factory = factory ?? (() => new T());

        for (int i = 0; i < preload; i++)
            _instances.Enqueue(_factory());
    }

    public T Get()
    {
        return _instances.Count > 0 ? _instances.Dequeue() : _factory();
    }
    public void Return(T item)
    {
        _instances.Enqueue(item);
    }
}
