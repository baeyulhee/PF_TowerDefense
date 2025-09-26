using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst ??= FindObjectOfType<T>();
                _inst ??= new GameObject(typeof(T).Name).AddComponent<T>();

                _inst.Init();
            }

            return _inst;
        }
    }
    public static bool IsValid => _inst != null;

    private void Awake()
    {
        if (_inst == null)
        {
            _inst = this as T;
            _inst.Init();
        }
        else if (_inst != this)
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (_inst == this)
            _inst = null;

        Release();
    }

    protected virtual void Init() { }
    protected virtual void Release() { }
}