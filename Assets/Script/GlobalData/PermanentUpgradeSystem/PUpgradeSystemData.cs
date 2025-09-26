using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PUSystem", menuName = "PUpgrade/PUpgradeSystem")]
public class PUpgradeSystemData : ScriptableObject
{
    [SerializeField] List<PUpgradeUnitData> _allUpgrades;
    public List<PUpgradeUnitData> AllUpgrades => _allUpgrades;
}
