using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUpgradeEntry : MonoBehaviour
{
    [SerializeField] Button _btnUpgrade;
    [SerializeField] TextMeshProUGUI _textUpgradeDescription;
    [SerializeField] TextMeshProUGUI _textUpgradeLevel;
    [SerializeField] TextMeshProUGUI _textUpgradeCost;

    private PUpgradeUnit _upgradeUnit;

    private void Start()
    {
        _btnUpgrade.onClick.AddListener(() =>
        {
            GameData.Inst.GameGold -= _upgradeUnit.Cost;
            Refresh();
        });
        Refresh();
    }

    public void Refresh()
    {
        _textUpgradeDescription.text = _upgradeUnit.Data.Description;
        _textUpgradeLevel.text = $"{_upgradeUnit.Level} / {_upgradeUnit.Data.MaxLevel}";
        _textUpgradeCost.text = _upgradeUnit.IsMaxLevel ? $"{_upgradeUnit.Cost}" : "-";
        _btnUpgrade.interactable = !_upgradeUnit.IsMaxLevel && GameData.Inst.GameGold >= _upgradeUnit.Cost;
    }

    public void Initialize(PUpgradeUnit upgradeUnit)
    {
        _upgradeUnit = upgradeUnit;
        Refresh();
    }
}
