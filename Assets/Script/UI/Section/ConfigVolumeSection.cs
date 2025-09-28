using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfigVolumeSection : MonoBehaviour
{
    [SerializeField] string _fieldName;
    [SerializeField] TextMeshProUGUI _textName;
    [SerializeField] Slider _sliderVolume;
    [SerializeField] Button _btnSoundSample;

    public ConfigVolumeSection SetVolume(float volumeValue)
    {
        _sliderVolume.value = volumeValue;
        return this;
    }
    public ConfigVolumeSection SetSliderMove(UnityAction<float> onSliderMove)
    {
        _sliderVolume.onValueChanged.AddListener(onSliderMove);
        return this;
    }
    public ConfigVolumeSection SetSamplePlay(UnityAction onSamplePlay)
    {
        _btnSoundSample.onClick.AddListener(onSamplePlay);
        return this;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_textName != null)
            _textName.text = _fieldName;
    }
#endif
}
