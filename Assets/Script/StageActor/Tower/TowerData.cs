using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObject/Tower")]
public class TowerData : ScriptableObject
{
    [SerializeField] string _key;
    public string Key => _key;

    [SerializeField] Tower _prefab;
    public Tower Prefab => _prefab;

    [SerializeField] int _cost;
    public int Cost => _cost;

    [SerializeField] TowerUpgradeUnitData _upgradeUnit;
    public TowerUpgradeUnitData UpgradeUnit => _upgradeUnit;
}