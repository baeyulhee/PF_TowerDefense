using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveStartButton : MonoBehaviour
{
    [SerializeField] Button _btnWaveStart;
    [SerializeField] TextMeshProUGUI _textWaveRemainTime;

    private void Start()
    {
        _btnWaveStart.onClick.AddListener(() => PlayerRequestManager.Inst.RequestWaveStart());
    }
    private void Update()
    {
        _btnWaveStart.interactable = StageData.Inst.WaveIsWaiting;
        _textWaveRemainTime.text = (StageData.Inst.WaveRemainTime > 0f) ? $"{StageData.Inst.WaveRemainTime:0.00}" : "";
    }
}
