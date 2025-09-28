using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildPanel : UIPanel
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

    public TowerBuildPanel SetTowerSlot(TowerSlot towerSlot)
    {
        _focusTowerSlot = towerSlot;
        return this;
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
            btnBuild.onClick.AddListener(() =>
            {
                PlayerRequestManager.Inst.RequestTowerBuild(_focusTowerSlot, item, cost);
                Hide();
            });

            UIHover hoverBuild = UIHover.Get(btnBuild.gameObject);
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

            TextMeshProUGUI nameText = btnBuild.GetComponentInChildren<TextMeshProUGUI>();
            nameText.text = $"{item.Key} : ${cost}";

            _btnBuildList.Add(btnBuild);
        }

        _btnClose.onClick.AddListener(Close);
    }
    public override void Show()
    {
        base.Show();
        
        StageData.Inst.OnPointChanged += OnPointChanged;
        UIRefData.Inst.PreviewCost = 0;
        ClearPreviewTower();

        Refresh();
    }
    public override void Hide()
    {
        base.Hide();

        _focusTowerSlot = null;
        StageData.Inst.OnPointChanged -= OnPointChanged;
        UIRefData.Inst.PreviewCost = 0;
        ClearPreviewTower();
    }
    
    private void Refresh()
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
