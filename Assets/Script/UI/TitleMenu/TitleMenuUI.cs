using UnityEngine;
using UnityEngine.UI;

public class TitleMenuUI : PanelUI
{
    [SerializeField] Button _btnStart;
    [SerializeField] Button _btnUpgrade;
    [SerializeField] Button _btnConfig;
    [SerializeField] Button _btnExit;

    private void Start()
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
