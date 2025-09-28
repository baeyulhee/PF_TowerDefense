using System;
using UnityEngine;

public abstract class VisualSequence : MonoBehaviour
{
    public event Action OnComplete;
    protected void InvokeComplete() => OnComplete?.Invoke();

    public abstract void Play();
}
