using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerBuildUI : PanelUI
{
    [SerializeField] Material _previewMaterial;
    private Dictionary<TowerData, GameObject> _towerPreviews = new();
    private GameObject _currentTowerPreview;

    [SerializeField] RectTransform _btnBuildLayout;
    [SerializeField] Button _btnBuildPrefab;

    List<Button> _btnBuildList;
    [SerializeField] Button _btnClose;

    [SerializeField] List<TowerData> _towerDataList;
    private TowerSlot _focusTowerSlot;

    public void SetData(TowerSlot towerSlot)
    {
        _focusTowerSlot = towerSlot;
    }
    public override void Init()
    {
        _towerPreviews.Clear();
        foreach (var item in _towerDataList)
        {
            GameObject go = MeshCombiner.CombineMeshes(item.Prefab.gameObject, _previewMaterial);
            go.transform.SetParent(transform);
            go.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            _towerPreviews.Add(item, go);
        }

        _btnBuildList = new List<Button>();
        foreach (var item in _towerDataList)
        {
            int cost = (int)(item.Cost * (1 - GameData.Inst.UpgradeDic[PUEnum.TowerCostDiscount]));

            Button btnBuild = Instantiate(_btnBuildPrefab, _btnBuildLayout);
            UIHover hoverBuild = UIHover.Get(btnBuild.gameObject);

            TextMeshProUGUI nameText = btnBuild.GetComponentInChildren<TextMeshProUGUI>();
            EventTrigger eventBuild = btnBuild.GetComponentInChildren<EventTrigger>();

            nameText.text = $"{item.Key} : ${cost}";
            btnBuild.onClick.AddListener(() =>
            {
                PlayerRequestManager.Inst.RequestTowerBuild(_focusTowerSlot, item, cost);
                Hide();
            });
            hoverBuild.onEnter.AddListener(() =>
            {
                UIRefData.Inst.PreviewCost = -cost;
                ShowPreviewTower(item);
            });
            hoverBuild.onExit.AddListener(() =>
            {
                UIRefData.Inst.PreviewCost = 0;
                ClearPreviewTower();
            });

            _btnBuildList.Add(btnBuild);
        }

        _btnClose.onClick.AddListener(Hide);
    }
    public override void Show()
    {
        base.Show();
        Refresh();

        StageData.Inst.OnPointChanged += OnPointChanged;
    }
    public override void Hide()
    {
        base.Hide();

        StageData.Inst.OnPointChanged -= OnPointChanged;

        _focusTowerSlot = null;
        UIRefData.Inst.PreviewCost = 0;

        ClearPreviewTower();
    }
    protected override void Refresh()
    {
        for (int i = 0; i < _btnBuildList.Count; i++)
            _btnBuildList[i].interactable = (StageData.Inst.Point >= _towerDataList[i].Cost);
    }

    private void ShowPreviewTower(TowerData towerData)
    {
        ClearPreviewTower();

        _currentTowerPreview = _towerPreviews[towerData];
        _currentTowerPreview.SetActive(true);
        _currentTowerPreview.transform.SetParent(null, false);
        _currentTowerPreview.transform.position = _focusTowerSlot.transform.position;
    }
    private void ClearPreviewTower()
    {
        if (_currentTowerPreview != null)
        {
            _currentTowerPreview.SetActive(false);
            _currentTowerPreview.transform.SetParent(transform, false);
        }
            
    }

    private void OnPointChanged(int value)
    {
        Refresh();
    }
}
