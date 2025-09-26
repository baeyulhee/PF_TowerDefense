using UnityEngine;
using UnityEngine.UI;

public class ConfigUI : PanelUI
{
    [Header("Window Size")]
    [SerializeField] Toggle _tgWindow;
    [SerializeField] Toggle _tgFullScreen;

    [Header("Volume Setting")]
    [SerializeField] ConfigVolumeEntry _bgmVolume;
    [SerializeField] AudioClip _bgmSample;

    [SerializeField] ConfigVolumeEntry _sfxVolume;
    [SerializeField] AudioClip _sfxSample;

    [SerializeField] ConfigVolumeEntry _uiVolume;
    [SerializeField] AudioClip _uiSample;

    [Header("Close")]
    [SerializeField] Button _btnClose;

    public override void Init()
    {
        _tgWindow.onValueChanged.AddListener((bool val) =>
        {
            if (val)
                Screen.SetResolution(1600, 900, false);
        });
        _tgFullScreen.onValueChanged.AddListener((bool val) =>
        {
            if (val)
                Screen.SetResolution(1600, 900, true);
        });

        _bgmVolume.Initialize(ConfigData.Inst.VolumeBGM,
            (float val) => ConfigData.Inst.VolumeBGM = val,
            () => SoundManager.Inst.PlayBackgroundMusic(_bgmSample));

        _sfxVolume.Initialize(ConfigData.Inst.VolumeSFX,
            (float val) => ConfigData.Inst.VolumeSFX = val,
            () =>
            {
                SoundManager.Inst.StopBackgroundMusic();
                SoundManager.Inst.PlayBackgroundMusic(_sfxSample);
            });

        _uiVolume.Initialize(ConfigData.Inst.VolumeUI,
            (float val) => ConfigData.Inst.VolumeUI = val,
            () =>
            {
                SoundManager.Inst.StopBackgroundMusic();
                SoundManager.Inst.PlayBackgroundMusic(_uiSample);
            });

        _btnClose.onClick.AddListener(() =>
        {
            ConfigData.Inst.SaveData();
            SoundManager.Inst.StopBackgroundMusic();
            Hide();
        });
    }
}