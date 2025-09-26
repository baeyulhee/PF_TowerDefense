using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUpgradeUI : PanelUI
{
    [SerializeField] TextMeshProUGUI _textGameGold;
    [SerializeField] RectTransform _upgradeField;
    [SerializeField] Button _btnClose;

    [SerializeField] MainUpgradeEntry _upgradeEntryPrefab;
    private List<MainUpgradeEntry> _upgradeEntries = new();

    public override void Init()
    {
        foreach (var item in GameData.Inst.PUpgradeSystem.AllUpgrades.Values)
        {
            var entry = Instantiate(_upgradeEntryPrefab, _upgradeField);
            entry.Initialize(item);
            _upgradeEntries.Add(entry);
        }

        _btnClose.onClick.AddListener(Hide);
    }

    public override void Show()
    {
        base.Show();

        GameData.Inst.OnGameGoldChanged += OnGameGoldChanged;
        Refresh();
    }
    public override void Hide()
    {
        base.Hide();

        GameData.Inst.OnGameGoldChanged -= OnGameGoldChanged;
    }
    protected override void Refresh()
    {
        base.Refresh();

        foreach (var item in _upgradeEntries)
            item.Refresh();
        _textGameGold.text = $"GameGold : ${GameData.Inst.GameGold}";
    }

    private void OnGameGoldChanged(int value) => Refresh();
}