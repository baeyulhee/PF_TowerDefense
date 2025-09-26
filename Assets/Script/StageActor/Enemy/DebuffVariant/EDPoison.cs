using UnityEngine;

public class EDPoison : EnemyDebuff
{
    private const float POISON_INTERVAL = 1f;
    private float _poisonTimer;

    [SerializeField] float _poisonDamage;
    [SerializeField] float _duration;

    public EDPoison(float poisonDamage = 0f, float duration = 1f)
    {
        _poisonDamage = poisonDamage;
        _duration = duration;
    }

    public override void OnApply(Enemy target)
    {
        _isExpired = false;
        _poisonTimer = POISON_INTERVAL;
    }
    public override void OnStack(EnemyDebuff stackDebuff)
    {
        if (stackDebuff is not EDPoison stackPoison) return;

        _duration = Mathf.Max(_duration, stackPoison._duration);
    }
    public override void OnTick(Enemy target, float deltaTime)
    {
        if (_isExpired) return;

        if ((_poisonTimer -= deltaTime) < 0)
        {
            target.Damage(_poisonDamage);
            _poisonTimer = POISON_INTERVAL;
        }

        if ((_duration -= deltaTime) < 0)
            _isExpired = true;
    }
    public override void OnRemove(Enemy target) { }
}
