using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] PanelInstance<StageSelectUI> _stageSelectUI;
    [SerializeField] PanelInstance<MainUpgradeUI> _upgradeUI;
    [SerializeField] PanelInstance<ConfigUI> _configUI;

    private void Start()
    {
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
        if (!_stageSelectUI.Instance.IsActive)
            PanelShow(_stageSelectUI.Instance);

        if (_upgradeUI.Instance.IsActive)
            PanelHide(_upgradeUI.Instance);
        if (_configUI.Instance.IsActive)
            PanelHide(_configUI.Instance);
    }
    private void OnRequestMainUpgradeEvent(RequestMainUpgradeEvent evt)
    {
        if (!_upgradeUI.Instance.IsActive)
            PanelShow(_upgradeUI.Instance);

        if (_stageSelectUI.Instance.IsActive)
            PanelHide(_stageSelectUI.Instance);
        if (_configUI.Instance.IsActive)
            PanelHide(_configUI.Instance);
    }
    private void OnRequestConfigEvent(RequestConfigEvent evt)
    {
        if (!_configUI.Instance.IsActive)
            PanelShow(_configUI.Instance);

        if (_stageSelectUI.Instance.IsActive)
            PanelHide(_stageSelectUI.Instance);
        if (_upgradeUI.Instance.IsActive)
            PanelHide(_upgradeUI.Instance);
    }

    private void PanelShow(PanelUI ui)
    {
        ui.Show();
        ui.RectTransform.localPosition = Vector2.right * 1000f;
        ui.RectTransform.DOLocalMoveX(0f, 0.5f);
    }
    private void PanelHide(PanelUI ui)
    {
        ui.RectTransform.DOLocalMoveX(-1000f, 0.5f).SetEase(Ease.OutQuart).OnComplete(() => ui.Hide());
    }

    private void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        }
    }
}