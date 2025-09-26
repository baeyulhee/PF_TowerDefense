public class EnemyManager : MonoSingleton<EnemyManager>
{
    private MonoObjectPool<Enemy> _enemyPool = new();

    public Enemy Create(EnemyData data)
    {
        if (!_enemyPool.HasEntry(data.Key))
            _enemyPool.AddPoolEntry(data.Key, data.Prefab);

        Enemy enemy = _enemyPool.GetItem(data.Key);
        enemy.InitializeData(data);
        StageData.Inst.ActiveEnemies.Add(enemy);

        EventBus.Inst.Publish(new EnemyCreateEvent(enemy));

        return enemy;
    }
    public void Expire(Enemy obj)
    {
        _enemyPool.ReturnItem(obj);
        StageData.Inst.ActiveEnemies.Remove(obj);

        EventBus.Inst.Publish(new EnemyDestroyEvent(obj));
    }
}