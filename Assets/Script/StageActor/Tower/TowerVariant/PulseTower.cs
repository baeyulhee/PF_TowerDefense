using UnityEngine;

public class PulseTower : Tower
{
    [SerializeField] Pulse _pulsePrefab;

    protected override void IdleState()
    {
        _state = TowerState.Attack;
        _attackTimer = 1 / _stats[ETowerStat.Speed];
    }
    protected override void AttackState()
    {
        if ((_attackTimer -= Time.deltaTime) < 0)
        {
            FirePulse();
            _attackTimer = 1 / _stats[ETowerStat.Speed];
        }
    }

    private void FirePulse()
    {
        Pulse pulse = Poolable.TryGet(_pulsePrefab);
        pulse.transform.SetPositionAndRotation(transform.position, transform.rotation);
        pulse.Launch(AttackEnemy, _stats[ETowerStat.Range]);
    }
    private void AttackEnemy(Enemy enemy)
    {
        enemy.Damage(_stats[ETowerStat.Attack]);
    }
}