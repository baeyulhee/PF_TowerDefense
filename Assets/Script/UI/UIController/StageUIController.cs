using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class StageUIController : MonoBehaviour
{
    [SerializeField] RectTransform _rtRightArea;

    [SerializeField] PanelInstance<TowerBuildUI> _towerBuildUI;
    [SerializeField] PanelInstance<TowerModifyUI> _towerModifyUI;
    [SerializeField] PanelInstance<StageClearUI> _stageClearUI;
    [SerializeField] PanelInstance<StageFailUI> _stageFailUI;
    [SerializeField] PanelInstance<StageMenuUI> _stageMenuUI;
    [SerializeField] PanelInstance<ConfigUI> _configUI;

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
        _towerModifyUI.Instance.SetData(evt.Tower);
        _towerModifyUI.Instance.Show();

        _towerModifyUI.Instance.RectTransform.position = _towerModifyUI.Instance.RectTransform.rect.position + Vector2.right * 200f;
        _towerModifyUI.Instance.RectTransform.DOLocalMoveX(-100, 0.5f);
    }
    private void OnTowerSlotSelectEvent(TowerSlotSelectEvent evt)
    {
        _towerBuildUI.Instance.SetData(evt.TowerSlot);
        _towerBuildUI.Instance.Show();

        _towerBuildUI.Instance.RectTransform.localPosition = _towerBuildUI.Instance.RectTransform.rect.position + Vector2.right * 200f;
        _towerBuildUI.Instance.RectTransform.DOLocalMoveX(-100, 0.5f);
    }
    private void OnStageClearEvent(StageClearEvent evt)
    {
        ObjectUIElement.SetInteractable<ObjectUIElement>(false);

        _stageClearUI.Instance.Show();
    }
    private void OnStageFailEvent(StageFailEvent evt)
    {
        ObjectUIElement.SetInteractable<ObjectUIElement>(false);

        _stageFailUI.Instance.Show();
    }

    private void OnRequestStageMenuEvent(RequestStageMenuEvent evt)
    {
        _stageMenuUI.Instance.Show();
    }
    private void OnRequestConfigEvent(RequestConfigEvent evt)
    {
        _configUI.Instance.Show();
    }
}
