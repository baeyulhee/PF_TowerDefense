using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PUpgradeSystem
{
    [SerializeField] PUpgradeSystemData _data;
    [SerializeField] SerializableDictionary<PUpgradeUnitData, PUpgradeUnit> _allUpgrades;

    public PUpgradeSystem(PUpgradeSystemData data)
    {
        _data = data;

        _allUpgrades = new();
        foreach (var item in _data.AllUpgrades)
            _allUpgrades[item] = new(item);
    }

    public PUpgradeSystemData Data => _data;
    public IReadOnlyDictionary<PUpgradeUnitData, PUpgradeUnit> AllUpgrades => _allUpgrades.Dictionary;
}
