using System;
using UnityEngine;

[Serializable]
public class OptionalValue<T>
{
    [SerializeField] bool _isOverride = false;
    [SerializeField] T _value;

    public bool IsOverride => _isOverride;
    public T Value => _value;

    public OptionalValue(T value, bool isOverride = false)
    {
        _value = value;
        _isOverride = isOverride;
    }

    public bool TryGetValue(out T value)
    {
        value = _value;
        return _isOverride;
    }
}
