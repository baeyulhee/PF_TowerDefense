using UnityEngine;
using UnityEngine.UI;

public class PlaySpeedButton : MonoBehaviour
{
    [SerializeField] Button _btnPlaySpeed;
    [SerializeField] Image _imgPause;
    [SerializeField] Image _imgResume;

    private void Start()
    {
        _btnPlaySpeed.onClick.AddListener(() =>
        {
            if (StageData.Inst.IsPause)
                PlayerRequestManager.Inst.RequestGameResume();
            else
                PlayerRequestManager.Inst.RequestGamePause();
        });
    }
    private void Update()
    {
        bool isPause = StageData.Inst.IsPause;

        _imgPause.enabled = !isPause;
        _imgResume.enabled = isPause;
    }
}
