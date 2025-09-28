using UnityEngine;
using UnityEngine.UI;

public class ConfigPanel : UIPanel
{
    [Header("Window Size")]
    [SerializeField] Toggle _tgWindow;
    [SerializeField] Toggle _tgFullScreen;

    [Header("Volume Setting")]
    [SerializeField] ConfigVolumeSection _bgmVolume;
    [SerializeField] AudioClip _bgmSample;

    [SerializeField] ConfigVolumeSection _sfxVolume;
    [SerializeField] AudioClip _sfxSample;

    [SerializeField] ConfigVolumeSection _uiVolume;
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

        _bgmVolume.SetVolume(ConfigData.Inst.VolumeBGM)
                  .SetSliderMove((float val) => ConfigData.Inst.VolumeBGM = val)
                  .SetSamplePlay(() => SoundManager.Inst.PlayBackgroundMusic(_bgmSample));

        _sfxVolume.SetVolume(ConfigData.Inst.VolumeSFX)
                  .SetSliderMove((float val) => ConfigData.Inst.VolumeSFX = val)
                  .SetSamplePlay(() =>
                  {
                      SoundManager.Inst.StopBackgroundMusic();
                      SoundManager.Inst.PlaySoundEffect(_sfxSample);
                  });

        _uiVolume.SetVolume(ConfigData.Inst.VolumeUI)
                 .SetSliderMove((float val) => ConfigData.Inst.VolumeUI = val)
                 .SetSamplePlay(() =>
                 {
                      SoundManager.Inst.StopBackgroundMusic();
                      SoundManager.Inst.PlayUISound(_uiSample);
                 });

        _btnClose.onClick.AddListener(() =>
        {
            ConfigData.Inst.SaveData();
            SoundManager.Inst.StopBackgroundMusic();
            Close();
        });
    }
}