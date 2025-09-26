using System.Collections;
using UnityEngine;

public class TowerUpgradeUnit
{
    [SerializeField] TowerUpgradeUnitData _data;
    [SerializeField] int _level;

    public TowerUpgradeUnit(TowerUpgradeUnitData data)
    {
        _data = data;
        _level = 0;
    }

    public TowerUpgradeUnitData Data => _data;
    public int Level => _level;
    public int MaxLevel => _data.MaxLevel;
    public int Cost => Level < _data.MaxLevel ? _data.CostTable[Level] : 0;

    public void ApplyUpgrade(Tower tower, int increaseLevel = 0)
    {
        int _validIncreaseLevel = Mathf.Min(increaseLevel, _data.MaxLevel - _level);
        _level += _validIncreaseLevel;

        foreach (var pair in _data.StatTable)
            tower.Stats[pair.Key] = pair.Value[_level];
    }
}