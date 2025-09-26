using System.Collections.Generic;
using UnityEngine;

public class EnemyWorldUI : MonoBehaviour
{
    private readonly Vector3 HPBAR_OFFSET = new Vector3(0, 10f, 0);
    private const string HPBAR_KEY = nameof(HPBAR_KEY);

    [SerializeField] UIGauge _hpGaugePrefab;

    private MonoObjectPool<UIGauge> _hpGaugePool = new();
    private Dictionary<Enemy, UIGauge> _hpGauges = new();

    private void Start()
    {
        EventBus.Inst.Subscribe<EnemyCreateEvent>(OnEnemyCreated);
        EventBus.Inst.Subscribe<EnemyDestroyEvent>(OnEnemyDestroyed);

        _hpGaugePool.AddPoolEntry(HPBAR_KEY, _hpGaugePrefab);
    }
    private void OnDestroy()
    {
        EventBus.Inst.UnSubscribe<EnemyCreateEvent>(OnEnemyCreated);
        EventBus.Inst.UnSubscribe<EnemyDestroyEvent>(OnEnemyDestroyed);
    }

    private void Update()
    {
        var cam = Camera.main;

        foreach (var item in _hpGauges)
        {
            Enemy enemy = item.Key;
            UIGauge hpGauge = item.Value;

            hpGauge.transform.position = cam.WorldToScreenPoint(enemy.transform.position) + HPBAR_OFFSET;

            item.Value.CurrentValue = item.Key.CurrentHp;
        }
    }

    private void OnEnemyCreated(EnemyCreateEvent evt)
    {
        UIGauge newHpGauge = _hpGaugePool.GetItem(HPBAR_KEY);
        newHpGauge.MaxValue = evt.Enemy.MaxHp;
        newHpGauge.CurrentValue = evt.Enemy.CurrentHp;

        newHpGauge.transform.SetParent(transform, false);

        _hpGauges.Add(evt.Enemy, newHpGauge);
    }
    private void OnEnemyDestroyed(EnemyDestroyEvent evt)
    {
        _hpGaugePool.ReturnItem(_hpGauges[evt.Enemy]);

        _hpGauges.Remove(evt.Enemy);
    }
}
