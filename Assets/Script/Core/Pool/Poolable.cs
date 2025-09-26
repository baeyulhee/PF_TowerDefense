using UnityEngine;

public class Poolable : MonoBehaviour
{
    public ObjectPool<Poolable> Pool { get; set; }

    protected virtual void Return()
    {
        Pool.Return(this);
    }

    public static GameObject TryGet(GameObject prefab)
    {
        if (prefab.TryGetComponent(out Poolable poolable))
            return PoolManager.Inst.GetItem(poolable).gameObject;
        else
            return Instantiate(prefab);
    }
    public static T TryGet<T>(GameObject prefab) where T : MonoBehaviour
    {
        return TryGet(prefab)?.GetComponent<T>();
    }
    public static T TryGet<T>(T prefab) where T : MonoBehaviour
    {
        return TryGet<T>(prefab.gameObject);
    }

    public static void TryReturn(GameObject gameObject)
    {
        var poolable = gameObject.GetComponent<Poolable>();
        if (poolable != null && poolable.Pool != null)
            poolable.Return();
        else
            Destroy(gameObject);
    }
    public static void TryReturn<T>(T item) where T : MonoBehaviour
    {
        TryReturn(item.gameObject);
    }
}
