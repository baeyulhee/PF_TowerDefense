public class FreezeTower : BasicTower
{
    protected override void AttackEnemy(Enemy enemy)
    {
        base.AttackEnemy(enemy);
        enemy.ApplyDebuff(new EDSlow(_stats[ETowerStat.SlowPercentage], _stats[ETowerStat.SlowDuration]));

        _state = TowerState.Detect;
    }
}
