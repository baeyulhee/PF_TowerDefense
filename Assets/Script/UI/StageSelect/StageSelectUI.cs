using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : PanelUI
{
    [SerializeField] RectTransform _btnStageLayout;
    [SerializeField] Button _btnStagePrefab;
    [SerializeField] Button _btnClose;

    [SerializeField] List<StageInitData> _stageInitList = new();
    private List<Button> _btnStageList = new();

    public override void Init()
    {
        _btnStageList = new List<Button>();
        foreach (var item in _stageInitList)
        {
            Button btn = Instantiate(_btnStagePrefab, _btnStageLayout);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = item.StageName;
            btn.onClick.AddListener(() =>
            {
                GameData.Inst.CurrentStage = item;

                SceneManager.LoadScene(item.StageName, LoadSceneMode.Single);
                SceneManager.LoadScene("StageInitScene", LoadSceneMode.Additive);
            });

            _btnStageList.Add(btn);
        }

        _btnClose.onClick.AddListener(Hide);
    }
}
