using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TUData", menuName = "TowerUpgrade/TowerUpgradeUnit")]
public class TowerUpgradeUnitData : ScriptableObject
{
    [SerializeField] int _maxLevel;
    [SerializeField] List<int> _costTable;
    [SerializeField] SerializableDictionary<ETowerStat, List<float>> _statTable;

    public int MaxLevel => _maxLevel;
    public List<int> CostTable => _costTable;
    public IReadOnlyDictionary<ETowerStat, List<float>> StatTable => _statTable.Dictionary;


#if UNITY_EDITOR
    private void OnValidate()
    {
        _costTable ??= new();

        if (_costTable.Count < _maxLevel)
            _costTable.AddRange(new int[_maxLevel - _costTable.Count]);
        else if (_costTable.Count > _maxLevel)
            _costTable.RemoveRange(_maxLevel, _costTable.Count - _maxLevel);

        _statTable ??= new();
        foreach (var list in _statTable.Values)
        {
            if (list.Count < _maxLevel)
                list.AddRange(new float[_maxLevel - list.Count + 1]);
            else if (list.Count > _maxLevel)
                list.RemoveRange(_maxLevel, list.Count - _maxLevel - 1);
        }
    }
#endif
}