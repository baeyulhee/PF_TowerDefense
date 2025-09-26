using UnityEngine;

[System.Serializable]
public class BasicTower : Tower
{
    [SerializeField] Transform _headTransform;
    [SerializeField] Transform _fireTransform;

    [SerializeField] Projectile _projectile;

    Target<Enemy> _target = new();

    protected override void IdleState()
    {
        _state = TowerState.Detect;
    }
    protected override void AttackState()
    {
        if (!_target.IsTargeting)
        {
            _state = TowerState.Idle;
            return;
        }

        if (!TowerDetection.DetectTargetInRange(this, _target.Data, _stats[ETowerStat.Range]))
        {
            _target.Detach();
            _state = TowerState.Idle;
            return;
        }

        _headTransform.LookAt(_target.Data.transform);

        if ((_attackTimer -= Time.deltaTime) < 0)
        {
            FireBullet();
            _attackTimer = 1 / _stats[ETowerStat.Speed];
        }
    }
    protected override void DetectState()
    {
        if (_target.TryAttach(TowerDetection.DetectByNear(this, _stats[ETowerStat.Range])))
        {
            _state = TowerState.Attack;
            _attackTimer = 1 / _stats[ETowerStat.Speed];
        }
    }

    protected virtual void FireBullet()
    {
        Projectile projectile = Poolable.TryGet(_projectile);
        projectile.transform.SetPositionAndRotation(_fireTransform.position, _headTransform.rotation);
        projectile.Launch(AttackEnemy, _target.Data);
    }
    protected virtual void AttackEnemy(Enemy enemy)
    {
        enemy.Damage(_stats[ETowerStat.Attack]);
    }
}