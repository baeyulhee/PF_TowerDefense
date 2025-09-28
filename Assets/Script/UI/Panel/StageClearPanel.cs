using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageClearPanel : UIPanel
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
}
