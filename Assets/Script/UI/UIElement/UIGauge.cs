using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    [SerializeField] Image _imgCurrentValue;

    float _maxValue = 1f;
    float _currentValue = 1f;

    public float MaxValue
    {
        get => _maxValue;
        set => _maxValue = value;
    }
    public float CurrentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            UpdateGauge();
        }
    }

    private void UpdateGauge()
    {
        _imgCurrentValue.fillAmount = _currentValue / _maxValue;
    }
}