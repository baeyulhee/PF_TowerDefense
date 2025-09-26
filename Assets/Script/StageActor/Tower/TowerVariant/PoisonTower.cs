public class PoisonTower : BasicTower
{
    protected override void AttackEnemy(Enemy enemy)
    {
        base.AttackEnemy(enemy);
        enemy.ApplyDebuff(new EDPoison(_stats[ETowerStat.PoisonAttack], _stats[ETowerStat.PoisonDuration]));

        _state = TowerState.Detect;
    }
}