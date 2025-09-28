using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] float _maxValue = 1f;
    [SerializeField] float _value = 1f;

    public Image FillImage { get => _fillImage; set => _fillImage = value; }
    public float MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            UpdateValue();
        }
    }
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            UpdateValue();
        }
    }

    private void UpdateValue()
    {
        _maxValue = Mathf.Max(0f, _maxValue);
        _value = Mathf.Clamp(_value, 0f, _maxValue);
        _fillImage.fillAmount = _value / _maxValue;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateValue();
    }
#endif
}