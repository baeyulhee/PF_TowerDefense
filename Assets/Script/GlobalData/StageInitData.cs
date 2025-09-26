using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageInitData : ScriptableObject
{
    [SerializeField] string _stageName;
    public string StageName => _stageName;

    [SerializeField] AudioClip _backgroundMusic;
    public AudioClip BackgroundMusic => _backgroundMusic;

    [SerializeField] int _initPoint;
    public int InitPoint => _initPoint;

    [SerializeField] int _stageReward;
    public int StageReward => _stageReward;

    public int WaveTotalCount => _spawnData.Count;

    [SerializeField] float _waveInterval;
    public float WaveInterval => _waveInterval;

    [SerializeField] float _coreHp;
    public float CoreHp => _coreHp;

    [SerializeField] List<SpawnInfo> _spawnData = new();
    public List<SpawnInfo> SpawnData => _spawnData;
}
