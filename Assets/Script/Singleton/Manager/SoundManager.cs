using UnityEngine;

public enum UISound
{
    Select,
    Cancel,
    Button,
    Toggle,
    Slider
}
public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioSource _bgMusicPlayer;
    [SerializeField] AudioSource _effectPlayer;
    [SerializeField] AudioSource _uiSoundPlayer;

    [SerializeField] SerializableDictionary<UISound, AudioClip> _uiSoundDict;

    protected override void Init()
    {
        _bgMusicPlayer ??= gameObject.AddComponent<AudioSource>();
        _effectPlayer ??= gameObject.AddComponent<AudioSource>();
        _uiSoundPlayer ??= gameObject.AddComponent<AudioSource>();

        SetBackgroundVolume(ConfigData.Inst.VolumeBGM);
        SetSoundEffectVolume(ConfigData.Inst.VolumeSFX);
        SetUISoundVolume(ConfigData.Inst.VolumeUI);

        ConfigData.Inst.OnVolumeBGMChanged += SetBackgroundVolume;
        ConfigData.Inst.OnVolumeSFXChanged += SetSoundEffectVolume;
        ConfigData.Inst.OnVolumeUIChanged += SetUISoundVolume;
    }
    protected override void Release()
    {
        if (ConfigData.IsValid)
        {
            ConfigData.Inst.OnVolumeBGMChanged -= SetBackgroundVolume;
            ConfigData.Inst.OnVolumeSFXChanged -= SetSoundEffectVolume;
            ConfigData.Inst.OnVolumeUIChanged -= SetUISoundVolume;
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        _bgMusicPlayer.clip = clip;
        _bgMusicPlayer.Play();
    }
    public void StopBackgroundMusic()
    {
        _bgMusicPlayer.Stop();
    }
    public void PlaySoundEffect(AudioClip clip)
    {
        _effectPlayer.PlayOneShot(clip);
    }
    public void PlayUISound(AudioClip clip)
    {
        _uiSoundPlayer.PlayOneShot(clip);
    }
    public void PlayUISound(UISound uiSoundIndex)
    {
        _uiSoundPlayer.PlayOneShot(_uiSoundDict[uiSoundIndex]);
    }

    private void SetBackgroundVolume(float value)
    {
        _bgMusicPlayer.volume = value;
    }
    private void SetSoundEffectVolume(float value)
    {
        _effectPlayer.volume = value;
    }
    private void SetUISoundVolume(float value)
    {
        _uiSoundPlayer.volume = value;
    }
}
