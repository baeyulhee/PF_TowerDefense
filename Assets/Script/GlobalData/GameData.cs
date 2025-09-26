using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SerializableGameData
{
    public int GameGold;
    public PUpgradeSystem PUPgradeSystem;

    public SerializableDictionary<string, int> UpgradeLevelDic = new();
}

public class GameData : MonoSingleton<GameData>
{
    private const string _defaultSaveFileName = "SaveFile";

    [SerializeField] GameInitData _initData;
    [SerializeField] ObsValue<int> _gameGold = new(0);
    [SerializeField] PUpgradeSystem _pUpgradeSystem;
    private Dictionary<PUEnum, float> _upgradeDic = new();

    public int GameGold
    {
        get => _gameGold.Value;
        set => _gameGold.Value = value;
    }
    public event Action<int> OnGameGoldChanged
    {
        add => _gameGold.OnValueChanged += value;
        remove => _gameGold.OnValueChanged -= value;
    }
    public PUpgradeSystem PUpgradeSystem => _pUpgradeSystem;
    public StageInitData CurrentStage { get; set; } = null;
    public Dictionary<PUEnum, float> UpgradeDic => _upgradeDic;

    public void InitializeData(GameInitData initData)
    {
        _initData = initData;
        Init();
    }
    protected override void Init()
    {
        if (_initData == null)
            return;

        _pUpgradeSystem = new(_initData.PUpgradeSystemData);
        _upgradeDic.Clear();
        foreach (PUEnum key in Enum.GetValues(typeof(PUEnum)))
            _upgradeDic[key] = 0f;
    }

    public void SaveData(string fileName = _defaultSaveFileName)
    {
        SerializableGameData data = new();

        data.GameGold = _gameGold.Value;
        data.PUPgradeSystem = _pUpgradeSystem;

        string ToJsonData = JsonUtility.ToJson(data);
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.json");

        File.WriteAllText(filePath, ToJsonData);
    }
    public void LoadData(string fileName = _defaultSaveFileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"{fileName}.json");
        SerializableGameData data;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SerializableGameData>(FromJsonData);

            _gameGold.Value = data.GameGold;
            _pUpgradeSystem = data.PUPgradeSystem;
        }
        else
            data = new();
    }
}
