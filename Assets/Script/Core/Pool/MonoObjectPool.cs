using System.Collections.Generic;
using UnityEngine;

public class MonoObjectPool<T> where T : MonoBehaviour
{
    private class PoolEntry
    {
        public T Prefab;
        public int Amount;
        public Queue<T> Instances;

        public PoolEntry(T prefab)
        {
            Prefab = prefab;
            Amount = 0;
            Instances = new Queue<T>();
        }
    }

    private Dictionary<string, PoolEntry> _pools = new();
    private Dictionary<T, string> _objToKey = new();

    T CreateNewItem(string key)
    {
        if (!_pools.ContainsKey(key))
            return null;

        _pools[key].Amount++;

        T newObject = GameObject.Instantiate(_pools[key].Prefab);
        newObject.name = $"{key}_{_pools[key].Amount}";
        newObject.gameObject.SetActive(false);

        _objToKey.Add(newObject, key);

        return newObject;
    }
    T GetPooledItem(string key)
    {
        if (!_pools.ContainsKey(key))
            return null;

        return _pools[key].Instances.Count == 0 ? CreateNewItem(key) : _pools[key].Instances.Dequeue();
    }

    public void AddPoolEntry(string key, T prefab)
    {
        if (_pools.ContainsKey(key))
            return;

        _pools.Add(key, new PoolEntry(prefab));
    }

    public bool HasEntry(string key)
    {
        return _pools.ContainsKey(key);
    }
    public bool HasEntry(T prefab)
    {
        foreach (var entry in _pools.Values)
        {
            if (entry.Prefab == prefab)
                return true;
        }
        return false;
    }

    public T GetItem(string key)
    {
        if (!_pools.ContainsKey(key))
            return null;

        T obj = GetPooledItem(key);
        obj.gameObject.SetActive(true);

        return obj;
    }
    public T GetItem(T prefab)
    {
        foreach (var index in _pools.Keys)
        {
            if (_pools[index].Prefab == prefab)
                return GetItem(index);
        }

        return null;
    }

    public void ReturnItem(T item)
    {
        if (!_objToKey.TryGetValue(item, out string key))
            return;

        item.gameObject.SetActive(false);
        _pools[key].Instances.Enqueue(item);
    }
}