using System;
using UnityEngine;

public class UIRefData : MonoSingleton<UIRefData>
{
    [SerializeField] private ObsValue<int> _previewCost = new(0);

    public int PreviewCost
    {
        get => _previewCost.Value;
        set => _previewCost.Value = value;
    }
    public event Action<int> OnPreviewCostChanged
    {
        add => _previewCost.OnValueChanged += value;
        remove => _previewCost.OnValueChanged -= value;
    }
}
