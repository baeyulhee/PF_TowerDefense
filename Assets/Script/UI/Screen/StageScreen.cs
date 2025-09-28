using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class StageScreen : MonoBehaviour
{
    [SerializeField] RectTransform _rtRightArea;

    [SerializeField] TowerBuildPanel _towerBuildPanel;
    [SerializeField] TowerModifyPanel _towerModifyPanel;

    [SerializeField] StageClearPanel _stageClearPanel;
    [SerializeField] StageFailPanel _stageFailPanel;
    [SerializeField] StageMenuPanel _stageMenuPanel;
    [SerializeField] ConfigPanel _configPanel;

    private void Start()
    {
        foreach (var item in FindObjectsOfType<TowerSlot>())
        {
            if (!item.TryGetComponent(out TowerSlotUIElement e))
                item.AddComponent<TowerSlotUIElement>();
        }
        EventBus.Inst.Subscribe<TowerCreateEvent>(OnTowerCreated);

        EventBus.Inst.Subscribe<TowerSelectEvent>(OnTowerSelectEvent);
        EventBus.Inst.Subscribe<TowerSlotSelectEvent>(OnTowerSlotSelectEvent);
        EventBus.Inst.Subscribe<StageClearEvent>(OnStageClearEvent);
        EventBus.Inst.Subscribe<StageFailEvent>(OnStageFailEvent);

        EventBus.Inst.Subscribe<RequestStageMenuEvent>(OnRequestStageMenuEvent);
        EventBus.Inst.Subscribe<RequestConfigEvent>(OnRequestConfigEvent);
    }
    private void OnDestroy()
    {
        EventBus.Inst.UnSubscribe<TowerCreateEvent>(OnTowerCreated);

        EventBus.Inst.UnSubscribe<TowerSelectEvent>(OnTowerSelectEvent);
        EventBus.Inst.UnSubscribe<TowerSlotSelectEvent>(OnTowerSlotSelectEvent);
        EventBus.Inst.UnSubscribe<StageClearEvent>(OnStageClearEvent);
        EventBus.Inst.UnSubscribe<StageFailEvent>(OnStageFailEvent);

        EventBus.Inst.UnSubscribe<RequestStageMenuEvent>(OnRequestStageMenuEvent);
        EventBus.Inst.UnSubscribe<RequestConfigEvent>(OnRequestConfigEvent);
    }

    private void OnTowerCreated(TowerCreateEvent evt)
    {
        if (!evt.Tower.TryGetComponent(out TowerUIElement e))
            evt.Tower.AddComponent<TowerUIElement>();
    }

    private void OnTowerSelectEvent(TowerSelectEvent evt)
    {
        _towerModifyPanel.SetTower(evt.Tower).Open();
        _rtRightArea.DOAnchorPosX(100, 0f);
        _rtRightArea.DOAnchorPosX(0, 0.5f);
    }
    private void OnTowerSlotSelectEvent(TowerSlotSelectEvent evt)
    {
        _towerBuildPanel.SetTowerSlot(evt.TowerSlot).Open();
        _rtRightArea.DOAnchorPosX(100, 0f);
        _rtRightArea.DOAnchorPosX(0, 0.5f);
    }
    private void OnStageClearEvent(StageClearEvent evt)
    {
        _stageClearPanel.Open();
    }
    private void OnStageFailEvent(StageFailEvent evt)
    {
        _stageFailPanel.Open();
    }

    private void OnRequestStageMenuEvent(RequestStageMenuEvent evt)
    {
        _stageMenuPanel.Open();
    }
    private void OnRequestConfigEvent(RequestConfigEvent evt)
    {
        _configPanel.Open();
    }
}
