using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerModifyUI : PanelUI
{
    [SerializeField] TextMeshProUGUI _textTowerName;
    [SerializeField] TextMeshProUGUI _textStat;
    [SerializeField] TextMeshProUGUI _textUpgrade;
    [SerializeField] TextMeshProUGUI _textSell;

    [SerializeField] Button _btnUpgrade;
    [SerializeField] Button _btnSell;
    [SerializeField] Button _btnClose;

    [SerializeField] GameObject _rangeVisualizer;
    private GameObject _rangeVisualizerInstance;

    private Tower _focusTower;

    private string Name => _focusTower.Key;
    private int SellPrice => _focusTower.SellPrice;
    private TowerStat Stats => _focusTower.Stats;
    private int UpgradeLevel => _focusTower.UpgradeSystem.Level;

    private int UpgradeMaxLevel => _focusTower.UpgradeSystem.MaxLevel;
    private int UpgradeCost => (int)(_focusTower.UpgradeSystem.Cost * (1 - GameData.Inst.UpgradeDic[PUEnum.TowerCostDiscount]));

    private void Update()
    {
        _rangeVisualizerInstance.transform.localScale = Vector3.one * _focusTower.Stats[ETowerStat.Range];
    }

    public void SetData(Tower tower)
    {
        _focusTower = tower;
    }
    public override void Init()
    {
        _rangeVisualizerInstance = Instantiate(_rangeVisualizer);

        _btnUpgrade.onClick.AddListener(() =>
        {
            PlayerRequestManager.Inst.RequestTowerUpgrade(_focusTower, UpgradeCost);
            Refresh();
        });
        UIHover.Get(_btnUpgrade.gameObject).onEnter.AddListener(() => UIRefData.Inst.PreviewCost = -UpgradeCost);
        UIHover.Get(_btnUpgrade.gameObject).onExit.AddListener(() => UIRefData.Inst.PreviewCost = 0);

        _btnSell.onClick.AddListener(() =>
        {
            PlayerRequestManager.Inst.RequestTowerSell(_focusTower, SellPrice);
            Hide();
        });
        UIHover.Get(_btnSell.gameObject).onEnter.AddListener(() => UIRefData.Inst.PreviewCost = SellPrice);
        UIHover.Get(_btnSell.gameObject).onExit.AddListener(() => UIRefData.Inst.PreviewCost = 0);

        _btnClose.onClick.AddListener(Hide);
    }
    public override void Show()
    {
        base.Show();

        _rangeVisualizerInstance.transform.position = _focusTower.transform.position;
        StageData.Inst.OnPointChanged += OnPointChanged;
    }
    public override void Hide()
    {
        base.Hide();

        StageData.Inst.OnPointChanged -= OnPointChanged;

        UIRefData.Inst.PreviewCost = 0;
        _rangeVisualizerInstance?.SetActive(false);
    }
    protected override void Refresh()
    {
        _textTowerName.text = Name;
        _textStat.text = "";
        foreach (var index in Stats.Keys)
            _textStat.text += $"{index} : {Stats[index]}\n";

        _textUpgrade.text = $"Upgrade : ${UpgradeCost}";
        _textSell.text = $"Sell : ${SellPrice}";

        _btnUpgrade.interactable = (StageData.Inst.Point >= UpgradeCost) && (UpgradeLevel < UpgradeMaxLevel);
    }

    private void OnPointChanged(int value)
    {
        _btnUpgrade.interactable = (value >= UpgradeCost) && (UpgradeLevel < UpgradeMaxLevel);
    }
}
