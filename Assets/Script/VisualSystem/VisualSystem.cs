using System;
using UnityEngine;

public abstract class VisualSystem : MonoBehaviour
{
    public event Action EndAction;
    protected void InvokeEndAction() => EndAction?.Invoke();

    public abstract void Play();
}
