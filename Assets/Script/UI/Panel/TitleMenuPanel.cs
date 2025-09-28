using UnityEngine;
using UnityEngine.UI;

public class TitleMenuPanel : UIPanel
{
    [SerializeField] Button _btnStart;
    [SerializeField] Button _btnUpgrade;
    [SerializeField] Button _btnConfig;
    [SerializeField] Button _btnExit;

    public override void Init()
    {
        _btnStart.onClick.AddListener(() => EventBus.Inst.Publish(new RequestStageSelectEvent()));
        _btnUpgrade.onClick.AddListener(() => EventBus.Inst.Publish(new RequestMainUpgradeEvent()));
        _btnConfig.onClick.AddListener(() => EventBus.Inst.Publish(new RequestConfigEvent()));
        _btnExit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
