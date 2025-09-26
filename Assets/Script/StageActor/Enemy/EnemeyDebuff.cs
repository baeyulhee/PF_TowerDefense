using System;

public abstract class EnemyDebuff
{
    protected bool _isExpired = false;
    public bool IsExpired => _isExpired;

    public abstract void OnApply(Enemy target);
    public abstract void OnStack(EnemyDebuff stackDebuff);
    public abstract void OnTick(Enemy target, float deltaTime);
    public abstract void OnRemove(Enemy target);
}
