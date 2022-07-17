using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private GameSettings _settings;

    public void MusicSliderChanged(float val)
    {
        _settings.musicVolume = val;
        MusicManager.UpdateVolumeBasedOnSettings();
    }

    public void SoundSliderChanged(float val)
    {
        _settings.soundVolume = val;
    }

    public void SaveSettings()
    {
        _settings.SaveToPrefs();
    }

    public void SyncUI()
    {
        _musicSlider.value = _settings.musicVolume;
        _sfxSlider.value = _settings.soundVolume;
    }

    // Start is called before the first frame update
    void Start()
    {
        _settings.LoadPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
