using System;
using UnityEngine;

public class Core : MonoBehaviour
{
    public event Action OnDead;

    [SerializeField] float _maxHp;
    public float MaxHp => _maxHp;

    float _hp;
    public float Hp => _hp;

    private bool _isDead;
    public bool IsDead => _isDead;

    private void Awake()
    {
        _hp = _maxHp;
        _isDead = false;
    }

    public void Initialize(float maxHp)
    {
        _maxHp = maxHp;
        _hp = _maxHp;
    }

    public virtual void Damage(float value)
    {
        if (_isDead) return;

        _hp -= Mathf.Min(value, _hp);

        if (_hp <= 0)
        {
            _isDead = true;
            OnDead?.Invoke();
        }
    }
}