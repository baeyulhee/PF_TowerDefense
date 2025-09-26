using UnityEngine;

public class EDSlow : EnemyDebuff
{
    [SerializeField] float _decreaseAmount;
    [SerializeField] float _duration;

    public EDSlow(float decreaseAmount = 0f, float duration = 1f)
    {
        _decreaseAmount = decreaseAmount;
        _duration = duration;
    }

    public override void OnApply(Enemy target)
    {
        _isExpired = false;
        target.SpeedModifier.Add(DecreaseSpeed);
    }
    public override void OnStack(EnemyDebuff stackDebuff)
    {
        if (stackDebuff is not EDSlow stackSlow) return;

        _duration = Mathf.Max(_duration, stackSlow._duration);
    }
    public override void OnTick(Enemy target, float deltaTime)
    {
        if (_isExpired) return;

        if ((_duration -= deltaTime) < 0)
            _isExpired = true;
    }
    public override void OnRemove(Enemy target)
    {
        target.SpeedModifier.Remove(DecreaseSpeed);
    }

    private float DecreaseSpeed(float val) => val * (1 - _decreaseAmount);
}
