using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<Poolable, ObjectPool<Poolable>> _pools;

    public Poolable GetItem(Poolable prefab)
    {
        if (!_pools.ContainsKey(prefab))
            _pools.Add(prefab, new ObjectPool<Poolable>(() => Instantiate(prefab)));

        ObjectPool<Poolable> pool = _pools[prefab];
        Poolable instance = pool.Get();
        instance.Pool = pool;

        instance.gameObject.SetActive(true);

        return instance;
    }
    public void ReturnItem(Poolable item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(transform, false);
        item.Pool.Return(item);
    }

    protected override void Init()
    {
        base.Init();

        _pools = new();
    }
}
