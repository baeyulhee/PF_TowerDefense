using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainUpgradeSection : MonoBehaviour
{
    [SerializeField] Button _btnUpgrade;
    [SerializeField] TextMeshProUGUI _textUpgradeDescription;
    [SerializeField] TextMeshProUGUI _textUpgradeLevel;
    [SerializeField] TextMeshProUGUI _textUpgradeCost;

    private PUpgradeUnit _upgradeUnit;

    public MainUpgradeSection SetUpgradeUnit(PUpgradeUnit upgradeUnit)
    {
        _upgradeUnit = upgradeUnit;
        Refresh();
        return this;
    }
    public MainUpgradeSection SetUpgradeAction(UnityAction onUpgradeButton)
    {
        _btnUpgrade.onClick.AddListener(onUpgradeButton);
        return this;
    }

    public void Refresh()
    {
        if (_upgradeUnit == null)
            return;

        _textUpgradeDescription.text = _upgradeUnit.Data.Description;
        _textUpgradeLevel.text = $"{_upgradeUnit.Level} / {_upgradeUnit.Data.MaxLevel}";
        _textUpgradeCost.text = _upgradeUnit.IsMaxLevel ? $"{_upgradeUnit.Cost}" : "-";
        _btnUpgrade.interactable = !_upgradeUnit.IsMaxLevel && GameData.Inst.GameGold >= _upgradeUnit.Cost;
    }
}
