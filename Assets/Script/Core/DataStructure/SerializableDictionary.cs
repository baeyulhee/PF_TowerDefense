using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] List<SerializableEntry<TKey, TValue>> _entries = new List<SerializableEntry<TKey, TValue>>();

    private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
    private List<SerializableEntry<TKey, TValue>> _duplicateList = new List<SerializableEntry<TKey, TValue>>();

    public Dictionary<TKey, TValue> Dictionary => _dictionary;
    public TValue this[TKey key] { get => _dictionary[key]; set => _dictionary[key] = value; }

    public void OnBeforeSerialize()
    {
        _entries.Clear();

        foreach (var pair in _dictionary)
            _entries.Add(new SerializableEntry<TKey, TValue>(pair.Key, pair.Value));
        foreach (var entry in _duplicateList)
            _entries.Add(entry);
    }
    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        _duplicateList = new List<SerializableEntry<TKey, TValue>>();

        foreach (SerializableEntry<TKey, TValue> entry in _entries)
        {
            if (entry.Key == null)
            {
                _duplicateList.Add(entry);
                continue;
            }

            if (!_dictionary.ContainsKey(entry.Key))
                _dictionary.Add(entry.Key, entry.Value);
            else
                _duplicateList.Add(entry);
        }
    }

    public Dictionary<TKey, TValue>.KeyCollection Keys => _dictionary.Keys;
    public Dictionary<TKey, TValue>.ValueCollection Values => _dictionary.Values;
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);
    public bool Add(TKey key, TValue value)
    {
        if (_dictionary.ContainsKey(key))
            return false;
        else
        {
            _dictionary.Add(key, value);
            return true;
        }
    }
    public bool Remove(TKey key)
    {
        bool removed = _dictionary.Remove(key);
        _duplicateList.RemoveAll(entry => (entry.Key.Equals(key)));

        return removed;
    }

    public bool Validate()
    {
        return _duplicateList.Count == 0;
    }
}

[Serializable]
public class SerializableEntry<TKey, TValue>
{
    [SerializeField] TKey _key;
    [SerializeField] TValue _value;

    public TKey Key => _key;
    public TValue Value => _value;

    public SerializableEntry(TKey key, TValue value)
    {
        _key = key;
        _value = value;
    }
}