using System;
using TMPro;
using UnityEngine;

public class StageInfoPanel : UIPanel
{
    [SerializeField] Gauge _gaugeCoreHp;
    [SerializeField] TextMeshProUGUI _textWave;
    [SerializeField] TextMeshProUGUI _textPoint;

    private void Update()
    {
        //_gaugeCoreHp.Value = StageData.Inst.Core.Hp;
    }

    public override void Init()
    {
        StageData.Inst.OnPointChanged += OnPointChanged;
        StageData.Inst.OnWaveCurrentCountChanged += OnCurrentWaveCountChanged;
        UIRefData.Inst.OnPreviewCostChanged += OnPreviewCostChanged;

        //_gaugeCoreHp.MaxValue = StageData.Inst.Hp
        _textWave.text = $"Wave : {StageData.Inst.WaveCurrentCount} / {StageData.Inst.WaveTotalCount}";
        //_gaugeCoreHp.Value = StageData.Inst.Core.Hp;

    }
    private void OnDestroy()
    {
        if (StageData.IsValid)
        {
            StageData.Inst.OnPointChanged -= OnPointChanged;
            StageData.Inst.OnWaveCurrentCountChanged -= OnCurrentWaveCountChanged;
        }
        if (UIRefData.IsValid)
            UIRefData.Inst.OnPreviewCostChanged -= OnPreviewCostChanged;
    }

    private void OnPointChanged(int value)
    {
        _textPoint.text = $"Point : {StageData.Inst.Point}";

        if (UIRefData.Inst.PreviewCost != 0)
        {
            bool positive = UIRefData.Inst.PreviewCost > 0;
            _textPoint.text += $" <color=#{(positive ? "00FF00" : "FF0000")}>{(positive ? '+' : '-')}{Math.Abs(UIRefData.Inst.PreviewCost)}";
        }
    }
    private void OnCurrentWaveCountChanged(int value)
    {
        _textWave.text = $"Wave : {value} / {StageData.Inst.WaveTotalCount}";
    }
    private void OnPreviewCostChanged(int value)
    {
        _textPoint.text = $"Point : {StageData.Inst.Point}";

        if (value != 0)
        {
            bool positive = value > 0;
            _textPoint.text += $" <color=#{(positive ? "00FF00" : "FF0000")}>{(positive ? '+' : '-')}{Math.Abs(value)}";
        }
    }


}
