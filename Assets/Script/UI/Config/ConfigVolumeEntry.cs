using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfigVolumeEntry : MonoBehaviour
{
    [SerializeField] string _fieldName;
    [SerializeField] TextMeshProUGUI _textName;
    [SerializeField] Slider _sliderVolume;
    [SerializeField] Button _btnSoundSample;

    public void Initialize(float volumeValue, UnityAction<float> onSliderMove, UnityAction onSamplePlay)
    {
        _sliderVolume.value = volumeValue;
        _sliderVolume.onValueChanged.AddListener(onSliderMove);
        _btnSoundSample.onClick.AddListener(onSamplePlay);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_textName != null)
            _textName.text = _fieldName;
    }
#endif
}
