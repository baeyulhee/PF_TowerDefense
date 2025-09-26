using System;
using UnityEngine;

public class Target<T> where T : MonoBehaviour, ITargetable
{
    private T _data;
    public T Data => _data;

    public bool IsTargeting => _data != null;
    public Transform Transform => _data.transform;

    public void Attach(T data)
    {
        if (_data != null)
            Detach();

        _data = data;
        _data.OnTargetDisappear += Detach;
    }
    public void Detach()
    {
        if (_data != null)
        {
            _data.OnTargetDisappear -= Detach;
            _data = null;
        }
    }

    public bool TryAttach(T data)
    {
        if (data == null)
            return false;
        else
        {
            Attach(data);
            return true;
        }
    }
}

public interface ITargetable
{
    public event Action OnTargetDisappear;
}