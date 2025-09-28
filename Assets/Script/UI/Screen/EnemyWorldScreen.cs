using System.Collections.Generic;
using UnityEngine;

public class EnemyWorldScreen : MonoBehaviour
{
    private readonly Vector3 HPBAR_OFFSET = new Vector3(0, 10f, 0);
    private const string HPBAR_KEY = nameof(HPBAR_KEY);

    [SerializeField] Gauge _hpGaugePrefab;

    private Dictionary<Enemy, Gauge> _hpGauges = new();

    private void Start()
    {
        EventBus.Inst.Subscribe<EnemyCreateEvent>(OnEnemyCreated);
        EventBus.Inst.Subscribe<EnemyDestroyEvent>(OnEnemyDestroyed);
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
            Gauge hpGauge = item.Value;

            hpGauge.transform.position = cam.WorldToScreenPoint(enemy.transform.position) + HPBAR_OFFSET;

            item.Value.Value = item.Key.CurrentHp;
        }
    }

    private void OnEnemyCreated(EnemyCreateEvent evt)
    {
        Gauge newHpGauge = Poolable.TryGet(_hpGaugePrefab);

        newHpGauge.MaxValue = evt.Enemy.MaxHp;
        newHpGauge.Value = evt.Enemy.CurrentHp;
        newHpGauge.transform.SetParent(transform, false);
        newHpGauge.gameObject.SetActive(true);

        _hpGauges.Add(evt.Enemy, newHpGauge);
    }
    private void OnEnemyDestroyed(EnemyDestroyEvent evt)
    {
        Gauge hpGauge = _hpGauges[evt.Enemy];

        hpGauge.gameObject.SetActive(false);
        Poolable.TryReturn(hpGauge);

        _hpGauges.Remove(evt.Enemy);
    }
}
