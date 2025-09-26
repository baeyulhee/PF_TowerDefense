using System;
using UnityEngine;

[Serializable]
public class ObsValue<T>
{
    [SerializeField] T _value;
    public T Value
    {
        get => _value;
        set
        {
            if (_value?.Equals(value) == true)
                return;

            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public event Action<T> OnValueChanged;

    public ObsValue(T value)
    {
        _value = value;
    }
}