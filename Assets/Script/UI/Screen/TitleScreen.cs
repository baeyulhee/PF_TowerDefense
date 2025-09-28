using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] StageSelectPanel _stageSelectPanel;
    [SerializeField] MainUpgradePanel _upgradePanel;
    [SerializeField] ConfigPanel _configPanel;

    private void Start()
    {
        _stageSelectPanel.SetOpenAction(() => PanelOpen(_stageSelectPanel));
        _stageSelectPanel.SetCloseAction(() => PanelClose(_stageSelectPanel));

        _upgradePanel.SetOpenAction(() => PanelOpen(_upgradePanel));
        _upgradePanel.SetCloseAction(() => PanelClose(_upgradePanel));

        _configPanel.SetOpenAction(() => PanelOpen(_configPanel));
        _configPanel.SetCloseAction(() => PanelClose(_configPanel));

        EventBus.Inst.Subscribe<RequestStageSelectEvent>(OnRequestStageSelectEvent);
        EventBus.Inst.Subscribe<RequestMainUpgradeEvent>(OnRequestMainUpgradeEvent);
        EventBus.Inst.Subscribe<RequestConfigEvent>(OnRequestConfigEvent);
    }
    private void OnDestroy()
    {
        EventBus.Inst.UnSubscribe<RequestStageSelectEvent>(OnRequestStageSelectEvent);
        EventBus.Inst.UnSubscribe<RequestMainUpgradeEvent>(OnRequestMainUpgradeEvent);
        EventBus.Inst.UnSubscribe<RequestConfigEvent>(OnRequestConfigEvent);
    }

    private void OnRequestStageSelectEvent(RequestStageSelectEvent evt)
    {
        _stageSelectPanel.Open();
    }
    private void OnRequestMainUpgradeEvent(RequestMainUpgradeEvent evt)
    {
        _upgradePanel.Open();
    }
    private void OnRequestConfigEvent(RequestConfigEvent evt)
    {
        _configPanel.Open();
    }

    private void PanelOpen(UIPanel ui)
    {
        ui.Show();
        ui.RectTransform.localPosition = Vector2.right * 1000f;
        ui.RectTransform.DOLocalMoveX(0f, 0.5f);
    }
    private void PanelClose(UIPanel ui)
    {
        ui.RectTransform.localPosition = Vector2.zero;
        ui.RectTransform.DOLocalMoveX(-1000f, 0.5f).OnComplete(ui.Hide);
    }
}