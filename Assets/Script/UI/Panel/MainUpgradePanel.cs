using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUpgradePanel : UIPanel
{
    [SerializeField] TextMeshProUGUI _textGameGold;
    [SerializeField] RectTransform _upgradeField;
    [SerializeField] Button _btnClose;

    [SerializeField] MainUpgradeSection _upgradeSectionPrefab;
    private List<MainUpgradeSection> _upgradeSections = new();

    public override void Init()
    {
        foreach (var item in GameData.Inst.PUpgradeSystem.AllUpgrades.Values)
        {
            var section = Instantiate(_upgradeSectionPrefab, _upgradeField);

            section.SetUpgradeUnit(item)
                   .SetUpgradeAction(() =>
                   {
                       GameData.Inst.GameGold -= item.Cost;
                       item.ApplyUpgrade(1);
                       section.Refresh();
                   });
            _upgradeSections.Add(section);
        }

        _btnClose.onClick.AddListener(Close);
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

    private void Refresh()
    {
        foreach (var item in _upgradeSections)
            item.Refresh();
        _textGameGold.text = $"GameGold : ${GameData.Inst.GameGold}";
    }

    private void OnGameGoldChanged(int value) => Refresh();
}