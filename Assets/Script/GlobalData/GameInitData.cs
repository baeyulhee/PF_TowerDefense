using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObject/GameData")]
public class GameInitData : ScriptableObject
{
    [SerializeField] private PUpgradeSystemData _pUpgradeSystemData;
    public PUpgradeSystemData PUpgradeSystemData => _pUpgradeSystemData;
}
