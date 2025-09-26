using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : PanelUI
{
    [SerializeField] Button _btnExit;
    [SerializeField] TextMeshProUGUI _textReward;

    public override void Init()
    {
        _btnExit.onClick.AddListener(PlayerRequestManager.Inst.RequestExitGame);
    }
    public override void Show()
    {
        base.Show();
        _textReward.text = $"Reward : {StageData.Inst.StageReward}";
    }

    private void OnStageClearEvent(StageClearEvent evt)
    {
        Show();
    }
}
