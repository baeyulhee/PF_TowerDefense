using System;
using UnityEngine;

[Serializable]
public class PUpgradeUnit
{
    [SerializeField] PUpgradeUnitData _data;
    [SerializeField] int _level;

    public PUpgradeUnit(PUpgradeUnitData data)
    {
        _data = data;
        _level = 0;
    }

    public PUpgradeUnitData Data => _data;
    public int Level => _level;
    public int MaxLevel => _data.MaxLevel;
    public bool IsMaxLevel => _level >= _data.MaxLevel;
    public int Cost => Level < _data.MaxLevel ? _data.CostTable[Level] : 0;

    public void ApplyUpgrade(int increaseLevel)
    {
        int _validIncreaseLevel = Mathf.Min(increaseLevel, _data.MaxLevel - _level);
        _level += _validIncreaseLevel;

        GameData.Inst.UpgradeDic[_data.Effect.Type] = _data.Effect.Value * _validIncreaseLevel;
    }
}
