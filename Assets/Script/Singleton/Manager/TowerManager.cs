public class TowerManager : MonoSingleton<TowerManager>
{
    private MonoObjectPool<Tower> _towerPool = new();

    public Tower Create(TowerData data)
    {
        if (!_towerPool.HasEntry(data.Key))
            _towerPool.AddPoolEntry(data.Key, data.Prefab);

        Tower tower = _towerPool.GetItem(data.Key);
        tower.InitializeData(data);
        StageData.Inst.ActiveTowers.Add(tower);

        EventBus.Inst.Publish(new TowerCreateEvent(tower));

        return tower;
    }
    public void Expire(Tower obj)
    {
        _towerPool.ReturnItem(obj);
        StageData.Inst.ActiveTowers.Remove(obj);

        EventBus.Inst.Publish(new TowerDestroyEvent(obj));
    }
}