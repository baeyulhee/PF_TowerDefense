using System.Collections.Generic;

public enum ETowerStat
{
    Attack,
    Speed,
    Range,
    SlowPercentage,
    SlowDuration,
    PoisonAttack,
    PoisonDuration
}
public class TowerStat
{
    private Dictionary<ETowerStat, float> _baseStat = new();

    public float this[ETowerStat index]
    {
        get => GetTowerStatValue(index);
        set => _baseStat[index] = value;
    }
    public Dictionary<ETowerStat, float>.KeyCollection Keys => _baseStat.Keys;

    private float GetTowerStatValue(ETowerStat index)
    {
        switch (index)
        {
            case ETowerStat.Attack:
                return _baseStat[index] * (1 + GameData.Inst.UpgradeDic[PUEnum.TowerAttackIncrease]);
            case ETowerStat.Speed:
                return _baseStat[index] * (1 + GameData.Inst.UpgradeDic[PUEnum.TowerSpeedIncrease]);
            case ETowerStat.Range:
                return _baseStat[index] * (1 + GameData.Inst.UpgradeDic[PUEnum.TowerRangeIncrease]);
            case ETowerStat.SlowPercentage:
            case ETowerStat.SlowDuration:
            case ETowerStat.PoisonAttack:
            case ETowerStat.PoisonDuration:
                return _baseStat[index];
            default:
                return 0f;
        }
    }

    public void Clear()
    {
        _baseStat.Clear();
    }
}