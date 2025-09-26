using UnityEngine;
using UnityEngine.UI;

public class StageFailUI : PanelUI
{
    [SerializeField] Button _btnExit;

    public override void Init()
    {
        _btnExit.onClick.AddListener(PlayerRequestManager.Inst.RequestExitGame);
    }
}
