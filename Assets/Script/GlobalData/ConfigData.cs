using System;
using UnityEngine;

public class ConfigData : MonoSingleton<ConfigData>
{
    [SerializeField] ObsValue<bool> _isFullScreen = new(true);
    [SerializeField] ObsValue<float> _volumeBGM = new(1f);
    [SerializeField] ObsValue<float> _volumeSFX = new(1f);
    [SerializeField] ObsValue<float> _volumeUI = new(1f);

    public bool IsFullScreen
    {
        get => _isFullScreen.Value;
        set => _isFullScreen.Value = value;
    }
    public event Action<bool> OnIsFullScreenChanged
    {
        add => _isFullScreen.OnValueChanged += value;
        remove => _isFullScreen.OnValueChanged -= value;
    }

    public float VolumeBGM
    {
        get => _volumeBGM.Value;
        set => _volumeBGM.Value = value;
    }
    public event Action<float> OnVolumeBGMChanged
    {
        add => _volumeBGM.OnValueChanged += value;
        remove => _volumeBGM.OnValueChanged -= value;
    }

    public float VolumeSFX
    {
        get => _volumeSFX.Value;
        set => _volumeSFX.Value = value;
    }
    public event Action<float> OnVolumeSFXChanged
    {
        add => _volumeSFX.OnValueChanged += value;
        remove => _volumeSFX.OnValueChanged -= value;
    }

    public float VolumeUI
    {
        get => _volumeUI.Value;
        set => _volumeUI.Value = value;
    }
    public event Action<float> OnVolumeUIChanged
    {
        add => _volumeUI.OnValueChanged += value;
        remove => _volumeUI.OnValueChanged -= value;
    }

    protected override void Init()
    {
        LoadData();
    }
    protected override void Release()
    {
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefsUtil.SetBool(PlayerPrefsKeys.FullScreen, _isFullScreen.Value);

        PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeBGM, _volumeBGM.Value);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeSFX, _volumeSFX.Value);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeUI, _volumeUI.Value);

        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKeys.FullScreen))
        {
            SaveData();
            return;
        }

        _isFullScreen.Value = PlayerPrefsUtil.GetBool(PlayerPrefsKeys.FullScreen, false);

        _volumeBGM.Value = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeBGM);
        _volumeSFX.Value = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeSFX);
        _volumeUI.Value = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeUI);
    }
}