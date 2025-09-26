using UnityEngine;
using UnityEngine.UI;

public class StageMenuUI : PanelUI
{
    [SerializeField] Button _btnResume;
    [SerializeField] Button _btnSettings;
    [SerializeField] Button _btnQuit;

    public override void Init()
    {
        _btnResume.onClick.AddListener(() => Hide());
        _btnSettings.onClick.AddListener(() => EventBus.Inst.Publish(new RequestConfigEvent()));
        _btnQuit.onClick.AddListener(() => PlayerRequestManager.Inst.RequestExitGame());
    }
}
