using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase
}

[RequireComponent(typeof(NavMeshAgent), typeof(Collider))]
public partial class Enemy : MonoBehaviour, ITargetable
{
    public event Action OnTargetDisappear;

    protected NavMeshAgent _agent;
    [SerializeField] ParticleSystem _deathParticle;

    protected EnemyData _data;

    protected float _currentHp;

    private ValueModifier<float> _speedModifier = new();
    protected Dictionary<Type, EnemyDebuff> _activeDebuffs = new();

    protected EnemyState _state;

    protected Core _target;
    private List<Vector3> _corners = new();
    private int _cornerIndex;

    #region Stat
    public string Key => _data.Key;
    public float MaxHp => _data.MaxHp;
    public float CurrentHp => _currentHp;
    public float Speed => _data.Speed;
    public ValueModifier<float> SpeedModifier => _speedModifier;

    public int Point => _data.Point;
    public Core Target => _target;
    #endregion

    public void InitializeData(EnemyData enemyData)
    {
        _agent ??= GetComponent<NavMeshAgent>();
        _data ??= enemyData;

        _currentHp = _data.MaxHp;

        _speedModifier.Clear();
        _activeDebuffs.Clear();

        _state = EnemyState.Idle;
    }
    public void Place(Vector3 position, Quaternion rotation)
    {
        _agent.Warp(position);
        transform.rotation = rotation;
    }

    private void OnDisable()
    {
        OnTargetDisappear?.Invoke();
        OnTargetDisappear = null;
    }

    private void Update()
    {
        switch (_state)
        {
            case EnemyState.Idle: IdleState(); break;
            case EnemyState.Chase: ChaseState(); break;
            default: break;
        }

        UpdateDebuff();
    }

    protected virtual void IdleState()
    {
        _target = FindObjectOfType<Core>();

        _agent.SetDestination(_target.transform.position);
        _agent.isStopped = false;

        _corners = EnemyRouteFinder.GetCenterPath(transform.position, _target.transform.position);
        _cornerIndex = 1;
        _agent.SetDestination(_corners[_cornerIndex]);

        _state = EnemyState.Chase;
    }
    protected virtual void ChaseState()
    {
        _agent.speed = _speedModifier.Result(Speed);

        if (_agent.remainingDistance < _agent.stoppingDistance)
        {
            if (_cornerIndex < _corners.Count - 1)
            {
                _cornerIndex++;
                _agent.SetDestination(_corners[_cornerIndex]);
            }
        }

        if (_target == null)
        {
            _state = EnemyState.Idle;
            return;
        }
        if (Vector3.Distance(transform.position, _target.transform.position) < _agent.stoppingDistance)
        {
            _target.Damage(1);
            Expire();
        }
    }

    private void UpdateDebuff()
    {
        List<Type> removeList = new();
        foreach (var item in _activeDebuffs)
        {
            var key = item.Key;
            var debuff = item.Value;

            debuff.OnTick(this, Time.deltaTime);
            if (debuff.IsExpired)
            {
                debuff.OnRemove(this);
                removeList.Add(key);
            }
        }
        foreach (var item in removeList)
            _activeDebuffs.Remove(item);
    }

    public virtual void Damage(float hitValue)
    {
        _currentHp -= Mathf.Min(hitValue, _currentHp);

        if (_currentHp <= 0)
        {
            EventBus.Inst.Publish(new EnemyKillEvent(this));
            Expire();
        }
    }
    public void ApplyDebuff(EnemyDebuff debuff)
    {
        Type debuffType = debuff.GetType();

        if (_activeDebuffs.TryGetValue(debuffType, out EnemyDebuff existingDebuff))
            existingDebuff.OnStack(debuff);
        else
        {
            _activeDebuffs.Add(debuffType, debuff);
            debuff.OnApply(this);
        }
    }

    protected void Expire()
    {
        EnemyManager.Inst.Expire(this);
    }
}