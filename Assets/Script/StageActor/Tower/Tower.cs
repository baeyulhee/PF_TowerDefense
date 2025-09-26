using System;
using UnityEngine;

public enum TowerState
{
    Idle,
    Attack,
    Detect,
    Expire
}

[System.Serializable]
public class Tower : MonoBehaviour
{
    public event Action OnRemoved;

    protected TowerData _data;

    protected TowerState _state;
    protected float _attackTimer;

    protected TowerStat _stats = new();
    protected TowerUpgradeUnit _upgradeSystem;

    protected int _spentCost = 0;


    public string Key => _data.Key;

    public TowerStat Stats => _stats;
    public TowerUpgradeUnit UpgradeSystem => _upgradeSystem;

    public int SpentCost { get => _spentCost; set => _spentCost = value; }
    public int SellPrice => _spentCost / 2;

    public virtual void InitializeData(TowerData towerData)
    {
        _data ??= towerData;

        _state = TowerState.Idle;
        _attackTimer = 0f;

        _stats.Clear();
        _upgradeSystem = new(_data.UpgradeUnit);
        _upgradeSystem.ApplyUpgrade(this);

        _spentCost = 0;
    }
    public void Place(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
    }

    void Update()
    {
        switch (_state)
        {
            case TowerState.Idle: IdleState(); break;
            case TowerState.Attack: AttackState(); break;
            case TowerState.Detect: DetectState(); break;
            case TowerState.Expire: ExpireState(); break;
            default: break;
        }
    }

    protected virtual void IdleState() { }
    protected virtual void AttackState() { }
    protected virtual void DetectState() { }
    protected virtual void ExpireState() { }

    public void Remove()
    {
        _state = TowerState.Expire;

        OnRemoved?.Invoke();
        TowerManager.Inst.Expire(this);
    }
}