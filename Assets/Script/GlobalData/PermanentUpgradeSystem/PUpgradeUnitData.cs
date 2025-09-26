using System;
using System.Collections.Generic;
using UnityEngine;

public enum PUEnum
{
    None,
    TowerAttackIncrease,
    TowerSpeedIncrease,
    TowerRangeIncrease,
    TowerCostDiscount
}

[CreateAssetMenu(fileName = "PUData", menuName = "PUpgrade/PUpgradeUnit")]
public class PUpgradeUnitData : ScriptableObject
{
    [Serializable]
    public struct EffectEntry
    {
        [SerializeField] public PUEnum Type;
        [SerializeField] public float Value;
    }

    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] int _maxLevel;
    [SerializeField] List<int> _costTable;
    [SerializeField] EffectEntry _effect;

    public string Name => _name;
    public string Description => _description;
    public int MaxLevel => _maxLevel;
    public List<int> CostTable => _costTable;
    public EffectEntry Effect => _effect;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _costTable ??= new();

        if (_costTable.Count < _maxLevel)
        {
            for (int i = _costTable.Count; i < _maxLevel; i++)
                _costTable.Add(0);
        }
        else if (_costTable.Count > _maxLevel)
            _costTable.RemoveRange(_maxLevel, _costTable.Count - _maxLevel);
    }
#endif
}