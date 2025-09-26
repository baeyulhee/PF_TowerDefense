using System;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoSingleton<StageData>
{
    [SerializeField] StageInitData _initData;

    [SerializeField] ObsValue<int> _point = new(0);
    [SerializeField] ObsValue<int> _waveCurrentCount = new(0);

    [SerializeField] ObsValue<bool> _waveIsWaiting = new(true);
    [SerializeField] float _waveRemainTime = 0f;

    [SerializeField] ObsValue<bool> _isPause = new(false);

    private Core _core;
    private EnemySpawner _spawner;
    private List<Tower> _activeTowers = new();
    private List<Enemy> _activeEnemies = new();

    public string StageName => _initData.StageName;
    public AudioClip BackgroundMusic => _initData.BackgroundMusic;

    public int InitPoint => _initData.InitPoint;
    public int StageReward => _initData.StageReward;
    public List<SpawnInfo> SpawnData => _initData.SpawnData;

    public int Point
    {
        get => _point.Value;
        set => _point.Value = value;
    }
    public event Action<int> OnPointChanged
    {
        add => _point.OnValueChanged += value;
        remove => _point.OnValueChanged -= value;
    }

    public int WaveTotalCount => _initData.WaveTotalCount;
    public int WaveCurrentCount
    {
        get => _waveCurrentCount.Value;
        set => _waveCurrentCount.Value = value;
    }
    public event Action<int> OnWaveCurrentCountChanged
    {
        add => _waveCurrentCount.OnValueChanged += value;
        remove => _waveCurrentCount.OnValueChanged -= value;
    }

    public float WaveInterval => _initData.WaveInterval;
    public bool WaveIsWaiting
    {
        get => _waveIsWaiting.Value;
        set => _waveIsWaiting.Value = value;
    }
    public event Action<bool> OnWaveIsWaitingChanged
    {
        add => _waveIsWaiting.OnValueChanged += value;
        remove => _waveIsWaiting.OnValueChanged -= value;
    }
    public float WaveRemainTime
    {
        get => _waveRemainTime;
        set => _waveRemainTime = value;
    }

    public bool IsPause
    {
        get => _isPause.Value;
        set => _isPause.Value = value;
    }
    public event Action<bool> OnIsPauseChanged
    {
        add => _isPause.OnValueChanged += value;
        remove => _isPause.OnValueChanged -= value;
    }

    public Core Core
    {
        get
        {
            _core ??= FindObjectOfType<Core>();
            return _core;
        }
    }
    public EnemySpawner Spawner
    {
        get
        {
            _spawner ??= FindObjectOfType<EnemySpawner>();
            return _spawner;
        }
    }
    public List<Tower> ActiveTowers => _activeTowers;
    public List<Enemy> ActiveEnemies => _activeEnemies;

    public void InitializeData(StageInitData data)
    {
        _initData = data;
        Init();
    }
    protected override void Init()
    {
        _initData = GameData.Inst.CurrentStage;
        _initData ??= ScriptableObject.CreateInstance<StageInitData>();

        _point.Value = _initData.InitPoint;

        _waveCurrentCount.Value = 0;
        _waveRemainTime = 0;

        _isPause.Value = false;

        _activeTowers.Clear();
        foreach (var item in FindObjectsOfType<Tower>())
            _activeTowers.Add(item);

        _activeEnemies.Clear();
        foreach (var item in FindObjectsOfType<Enemy>())
            _activeEnemies.Add(item);

        _core ??= FindObjectOfType<Core>();
        _core.Initialize(_initData.CoreHp);

        _spawner ??= FindObjectOfType<EnemySpawner>();
    }
}