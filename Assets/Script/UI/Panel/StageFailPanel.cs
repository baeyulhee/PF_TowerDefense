using UnityEngine;
using UnityEngine.UI;

public class StageFailPanel : UIPanel
{
    [SerializeField] Button _btnExit;

    public override void Init()
    {
        _btnExit.onClick.AddListener(PlayerRequestManager.Inst.RequestExitGame);
    }
}
